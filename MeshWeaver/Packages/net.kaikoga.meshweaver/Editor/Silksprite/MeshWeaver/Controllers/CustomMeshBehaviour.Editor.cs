using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Utils;
using Silksprite.MeshWeaver.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static Silksprite.MeshWeaver.Utils.Localization;

namespace Silksprite.MeshWeaver.Controllers
{
    [CustomEditor(typeof(CustomMeshBehaviour), true, isFallback = true)]
    [CanEditMultipleObjects]
    public class CustomMeshBehaviourEditor : MeshWeaverEditorBase
    {
        CustomMeshBehaviour _meshBehaviour;

        void OnEnable()
        {
            _meshBehaviour = (CustomMeshBehaviour)target;
        }

        public sealed override VisualElement CreateInspectorGUI()
        {
            var serializedUpdatesEveryFrame = serializedObject.FindProperty(nameof(CustomMeshBehaviour.updatesEveryFrame));
            var serializedProfile = serializedObject.FindProperty("profile"); // nameof(CustomMeshBehaviour.profile)
            var serializedMaterials = serializedObject.FindProperty(nameof(CustomMeshBehaviour.materials));
            var serializedMeshFilters = serializedObject.FindProperty(nameof(CustomMeshBehaviour.meshFilters));
            var serializedMeshColliders = serializedObject.FindProperty(nameof(CustomMeshBehaviour.meshColliders));

            var container = new VisualElement { name = "container" };
            container.Add(new IMGUIContainer(() =>
            {
                MeshWeaverControllerGUILayout.LangSelectorGUI();
                MeshWeaverControllerGUILayout.LodSelectorGUI(target);

                MeshWeaverGUILayout.PropertyField(serializedUpdatesEveryFrame, Loc("CustomMeshBehaviour.updatesEveryFrame"));
                MeshWeaverGUILayout.PropertyField(serializedProfile, Loc("CustomMeshBehaviour.profile"));
                MeshWeaverGUILayout.PropertyField(serializedMaterials, Loc("CustomMeshBehaviour.materials"));
                MeshWeaverGUILayout.PropertyField(serializedMeshFilters, Loc("CustomMeshBehaviour.meshFilters"));
                MeshWeaverGUILayout.PropertyField(serializedMeshColliders, Loc("CustomMeshBehaviour.meshColliders"));
                serializedObject.ApplyModifiedProperties();
            
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
            return container;
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