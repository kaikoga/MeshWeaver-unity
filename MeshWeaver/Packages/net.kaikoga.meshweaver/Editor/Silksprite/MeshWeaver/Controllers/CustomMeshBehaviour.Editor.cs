using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
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

        protected sealed override void PopulateInspectorGUI(VisualElement container)
        {
            container.Add(Div("properties", c =>
            {
                c.Add(Prop(nameof(CustomMeshBehaviour.updatesEveryFrame), Loc("CustomMeshBehaviour.updatesEveryFrame")));
                c.Add(Prop("profile", Loc("CustomMeshBehaviour.profile")));
                c.Add(Prop(nameof(CustomMeshBehaviour.materials), Loc("CustomMeshBehaviour.materials")));
                c.Add(Prop(nameof(CustomMeshBehaviour.meshFilters), Loc("CustomMeshBehaviour.meshFilters")));
                c.Add(Prop(nameof(CustomMeshBehaviour.meshColliders), Loc("CustomMeshBehaviour.meshColliders")));
            }));
            container.Add(new IMGUIContainer(() =>
            {
                if (_meshBehaviour is MeshBehaviour)
                {
                    MeshProviderMenus.Menu.ChildPopup(_meshBehaviour, Tr("Mesh Providers"));
                }

                if (GUILayout.Button(Tr("Collect Materials")))
                {
                    _meshBehaviour.CollectMaterials();
                }
                if (GUILayout.Button(Tr("Compile")))
                {
                    _meshBehaviour.Compile();
                }
                if (GUILayout.Button(Tr("Compile All Active")))
                {
                    foreach (var m in FindObjectsOfType<CustomMeshBehaviour>()) m.Compile();
                }
                if (HasSetupAsMeshRendererButton(_meshBehaviour) && GUILayout.Button(Tr("I am Mesh Renderer")))
                {
                    SetupAsMeshRenderer(_meshBehaviour);
                }

                if (HasCreateExporterButton(_meshBehaviour) && GUILayout.Button(Tr("Create Exporter")))
                {
                    CreateExporter(_meshBehaviour);
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
                var meshFilter = meshBehaviour.gameObject.AddComponent<MeshFilter>();
                meshBehaviour.meshFilters = meshBehaviour.meshFilters ?? new MeshFilter[] { };
                ArrayUtility.Add(ref meshBehaviour.meshFilters, meshFilter);
            }

            if (!meshBehaviour.GetComponent<MeshRenderer>())
            {
                meshBehaviour.gameObject.AddComponent<MeshRenderer>();
            }

            if (!meshBehaviour.GetComponent<MeshCollider>())
            {
                var meshCollider = meshBehaviour.gameObject.AddComponent<MeshCollider>();
                meshBehaviour.meshColliders = meshBehaviour.meshColliders ?? new MeshCollider[] { };
                ArrayUtility.Add(ref meshBehaviour.meshColliders, meshCollider);
            }
        }

        static void CreateExporter(CustomMeshBehaviour meshBehaviour)
        {
            if (!meshBehaviour.GetComponent<MeshBehaviourExporter>()) meshBehaviour.gameObject.AddComponent<MeshBehaviourExporter>();
        }
    }
}