using Silksprite.MeshBuilder.Controllers.Utils;
using UnityEditor;
using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers
{
    [CustomEditor(typeof(CustomMeshBehaviour), true, isFallback = true)]
    public class CustomMeshBehaviourEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var meshBehaviour = (CustomMeshBehaviour)target;
            if (meshBehaviour is MeshBehaviour concreteBehaviour)
            {
                MeshProviderMenus.Menu.PropertyField(meshBehaviour, ref concreteBehaviour.meshProviders);
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

        static void SetupAsMeshRenderer(CustomMeshBehaviour meshBehaviour)
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