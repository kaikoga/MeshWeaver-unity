using Silksprite.MeshWeaver.Controllers.Utils;
using Silksprite.MeshWeaver.Utils;
using UnityEditor;
using UnityEngine;
using static Silksprite.MeshWeaver.Utils.Localization;

namespace Silksprite.MeshWeaver.Controllers
{
    [CustomEditor(typeof(CustomMeshBehaviour), true, isFallback = true)]
    [CanEditMultipleObjects]
    public class CustomMeshBehaviourEditor : Editor
    {
        CustomMeshBehaviour _meshBehaviour;

        SerializedProperty _serializedUpdatesEveryFrame;
        SerializedProperty _serializedProfile;
        SerializedProperty _serializedMaterials;
        SerializedProperty _serializedMeshFilters;
        SerializedProperty _serializedMeshColliders;

        void OnEnable()
        {
            _meshBehaviour = (CustomMeshBehaviour)target;

            _serializedUpdatesEveryFrame = serializedObject.FindProperty(nameof(CustomMeshBehaviour.updatesEveryFrame));
            _serializedProfile = serializedObject.FindProperty("profile"); // nameof(CustomMeshBehaviour.profile)
            _serializedMaterials = serializedObject.FindProperty(nameof(CustomMeshBehaviour.materials));
            _serializedMeshFilters = serializedObject.FindProperty(nameof(CustomMeshBehaviour.meshFilters));
            _serializedMeshColliders = serializedObject.FindProperty(nameof(CustomMeshBehaviour.meshColliders));
        }

        public override void OnInspectorGUI()
        {
            MeshWeaverControllerGUILayout.LangSelectorGUI();
            MeshWeaverControllerGUILayout.LodSelectorGUI(target);

            MeshWeaverGUILayout.PropertyField(_serializedUpdatesEveryFrame, Loc("CustomMeshBehaviour.updatesEveryFrame"));
            MeshWeaverGUILayout.PropertyField(_serializedProfile, Loc("CustomMeshBehaviour.profile"));
            MeshWeaverGUILayout.PropertyField(_serializedMaterials, Loc("CustomMeshBehaviour.materials"));
            MeshWeaverGUILayout.PropertyField(_serializedMeshFilters, Loc("CustomMeshBehaviour.meshFilters"));
            MeshWeaverGUILayout.PropertyField(_serializedMeshColliders, Loc("CustomMeshBehaviour.meshColliders"));
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