using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Utils;
using Silksprite.MeshWeaver.GUIActions;
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
            container.Add(new Div(c =>
            {
                c.Add(Prop(nameof(CustomMeshBehaviour.updatesEveryFrame), Loc("CustomMeshBehaviour.updatesEveryFrame")));
                c.Add(Prop("profile", Loc("CustomMeshBehaviour.profile")));
                c.Add(Prop(nameof(CustomMeshBehaviour.materials), Loc("CustomMeshBehaviour.materials")));
            }));
            if (_meshBehaviour is MeshBehaviour)
            {
                container.Add(MeshProviderMenus.Menu.ToGUIAction(_meshBehaviour, Loc("Mesh Providers")));
            }

            container.Add(new LocButton(Loc("Collect Materials"), () => { _meshBehaviour.CollectMaterials(); }));
            container.Add(new LocButton(Loc("Compile"), () => { _meshBehaviour.Compile(); }));
            container.Add(new LocButton(Loc("Compile All Active"), () =>
            {
                foreach (var m in FindObjectsOfType<CustomMeshBehaviour>()) m.Compile();
            }));

            if (HasSetupAsMeshRendererButton(_meshBehaviour))
            {
                container.Add(new LocButton(Loc("I am Mesh Renderer"), () => { SetupAsMeshRenderer(_meshBehaviour); }));
            }

            if (HasCreateExporterButton(_meshBehaviour))
            {
                container.Add(new LocButton(Loc("Create Exporter"), () => { CreateExporter(_meshBehaviour); }));
            }
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
                meshBehaviour.gameObject.AddComponent<MeshFilter>();
            }

            if (!meshBehaviour.GetComponent<MeshRenderer>())
            {
                meshBehaviour.gameObject.AddComponent<MeshRenderer>();
            }

            if (!meshBehaviour.GetComponent<MeshCollider>())
            {
                meshBehaviour.gameObject.AddComponent<MeshCollider>();
            }
        }

        static void CreateExporter(CustomMeshBehaviour meshBehaviour)
        {
            if (!meshBehaviour.GetComponent<MeshBehaviourExporter>()) meshBehaviour.gameObject.AddComponent<MeshBehaviourExporter>();
        }
    }
}