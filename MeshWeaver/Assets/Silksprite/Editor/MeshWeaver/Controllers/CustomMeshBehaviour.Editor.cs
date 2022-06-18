using Silksprite.MeshWeaver.Controllers.Utils;
using UnityEditor;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers
{
    [CustomEditor(typeof(CustomMeshBehaviour), true, isFallback = true)]
    [CanEditMultipleObjects]
    public class CustomMeshBehaviourEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var meshBehaviour = (CustomMeshBehaviour)target;
            if (meshBehaviour is MeshBehaviour)
            {
                MeshProviderMenus.Menu.ChildPopup(meshBehaviour);
            }

            if (GUILayout.Button("Collect Materials"))
            {
                meshBehaviour.CollectMaterials();
            }
            if (GUILayout.Button("Compile"))
            {
                meshBehaviour.Compile();
            }
            if (HasSetupAsMeshRendererButton(meshBehaviour) && GUILayout.Button("I am Mesh Renderer"))
            {
                SetupAsMeshRenderer(meshBehaviour);
            }

            if (HasCreateExporterButton(meshBehaviour) && GUILayout.Button("Create Exporter"))
            {
                CreateExporter(meshBehaviour);
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