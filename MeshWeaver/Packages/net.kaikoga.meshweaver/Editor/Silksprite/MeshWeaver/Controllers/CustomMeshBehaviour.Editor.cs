using System;
using System.Linq;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Utils;
using Silksprite.MeshWeaver.Extensions;
using Silksprite.MeshWeaver.GUIActions;
using Silksprite.MeshWeaver.GUIActions.Events;
using Silksprite.MeshWeaver.GUIActions.Extensions;
using UnityEditor;
using UnityEngine;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers
{
    [CustomEditor(typeof(CustomMeshBehaviour), true, isFallback = true)]
    [CanEditMultipleObjects]
    public class CustomMeshBehaviourEditor : MeshWeaverEditorBase
    {
        protected override bool IsMainComponentEditor => true;

        CustomMeshBehaviour _meshBehaviour;

        void OnEnable()
        {
            _meshBehaviour = (CustomMeshBehaviour)target;
        }

        protected sealed override void PopulateInspectorGUI(GUIContainer container)
        {
            var onRefresh = new Dispatcher<RefreshEvent>();

            container.Add(new Div(c =>
            {
                // TODO: how to expose CustomMeshBehaviour.updatesEveryFrame
                // c.Add(Prop(nameof(CustomMeshBehaviour.updatesEveryFrame), Loc("CustomMeshBehaviour.updatesEveryFrame")));

                if (MeshWeaverSettings.Current.profiles.Length == 0) MeshWeaverSettings.Current.ResetDefaultProfiles();
                var options = MeshWeaverSettings.Current.profiles.Select(prof => _Loc(prof.name))
                    .Concat(new []{ Loc("CustomMeshBehaviour.customProfile") }).ToArray();
                var customProfileIndex = MeshWeaverSettings.Current.profiles.Length;
                var profileIndex = Array.IndexOf(MeshWeaverSettings.Current.profiles, serializedObject.FindProperty("profile").objectReferenceValue);
                if (profileIndex < 0) profileIndex = customProfileIndex; 
                var profilesPopup = new LocPopup(Loc("MeshWeaverSettings.profiles"),
                    profileIndex,
                    options);
                profilesPopup.onChanged.Add(_ =>
                {
                    if (profilesPopup.value < customProfileIndex)
                    {
                        serializedObject.FindProperty("profile").objectReferenceValue = MeshWeaverSettings.Current.profiles[profilesPopup.value];
                    }
                    serializedObject.ApplyModifiedProperties();
                    onRefresh.Invoke();
                });
                c.Add(profilesPopup);
                c.Add(Prop("profile", Loc("CustomMeshBehaviour.customProfile"))
                    .WithDisplayOnRefresh(onRefresh, () => profilesPopup.value == customProfileIndex));

                var lockMaterials = Prop(nameof(CustomMeshBehaviour.overrideMaterials), Loc("CustomMeshBehaviour.overrideMaterials"));
                lockMaterials.RegisterPropertyChangedCallback<bool>(changed => onRefresh.Invoke());
                c.Add(lockMaterials);
                c.Add(Prop(nameof(CustomMeshBehaviour.materials), Loc("CustomMeshBehaviour.materials"))
                    .WithEnableOnRefresh(onRefresh, () => _meshBehaviour.overrideMaterials));
            }));
            if (_meshBehaviour is MeshBehaviour)
            {
                container.Add(MeshProviderMenus.Menu.ToGUIAction(_meshBehaviour, Loc("Mesh Providers")));
            }

            container.Add(new HDiv(c =>
            {
                c.Add(new LocButton(Loc("Compile"), () => { _meshBehaviour.Compile(); }));
                c.Add(new LocButton(Loc("Compile All Active"), () =>
                {
                    foreach (var m in FindObjectsOfType<CustomMeshBehaviour>()) m.Compile();
                }));
            }));

            container.Add(new HDiv(c =>
            {
                if (HasSetupAsMeshRendererButton(_meshBehaviour))
                {
                    c.Add(new LocButton(Loc("I am Mesh Renderer"), () => { SetupAsMeshRenderer(_meshBehaviour); }));
                }
                if (HasCreateExporterButton(_meshBehaviour))
                {
                    c.Add(new LocButton(Loc("Create Exporter"), () => { CreateExporter(_meshBehaviour); }));
                }
            }));
        }

        static bool HasSetupAsMeshRendererButton(CustomMeshBehaviour meshBehaviour)
        {
            return !meshBehaviour.GetComponent<MeshFilter>() ||
                   !meshBehaviour.GetComponent<MeshRenderer>() ||
                   !meshBehaviour.GetComponent<MeshCollider>();
        }

        static bool HasCreateExporterButton(CustomMeshBehaviour meshBehaviour)
        {
            return !meshBehaviour.GetComponent<MeshBehaviourExporter>();
        }

        public static void SetupAsMeshRenderer(CustomMeshBehaviour meshBehaviour)
        {
            if (!meshBehaviour.GetComponent<MeshFilter>())
            {
                Undo.AddComponent<MeshFilter>(meshBehaviour.gameObject);
            }

            if (!meshBehaviour.GetComponent<MeshRenderer>())
            {
                Undo.AddComponent<MeshRenderer>(meshBehaviour.gameObject);
            }

            if (!meshBehaviour.GetComponent<MeshCollider>())
            {
                Undo.AddComponent<MeshCollider>(meshBehaviour.gameObject);
            }
        }

        static void CreateExporter(CustomMeshBehaviour meshBehaviour)
        {
            if (!meshBehaviour.GetComponent<MeshBehaviourExporter>()) meshBehaviour.gameObject.AddComponent<MeshBehaviourExporter>();
        }
    }
}