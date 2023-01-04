using Silksprite.MeshWeaver.Controllers.Utils;
using Silksprite.MeshWeaver.Scopes;
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
        public override void OnInspectorGUI()
        {
            MeshWeaverControllerGUI.LodSelectorGUI(target);

            using (new BackgroundColorScope(Color.magenta))
            {
                MeshWeaverGUI.Header(Tr("Fallback Inspector"));
                base.OnInspectorGUI();
            }

            var meshBehaviour = (CustomMeshBehaviour)target;
            if (meshBehaviour is MeshBehaviour)
            {
                MeshProviderMenus.Menu.ChildPopup(meshBehaviour, "Mesh Providers");
            }

            if (GUILayout.Button(Tr("Collect Materials")))
            {
                meshBehaviour.CollectMaterials();
            }
            if (GUILayout.Button(Tr("Compile")))
            {
                meshBehaviour.Compile();
            }
            if (GUILayout.Button(Tr("Compile All Active")))
            {
                foreach (var m in FindObjectsOfType<CustomMeshBehaviour>()) m.Compile();
            }
            if (HasSetupAsMeshRendererButton(meshBehaviour) && GUILayout.Button(Tr("I am Mesh Renderer")))
            {
                SetupAsMeshRenderer(meshBehaviour);
            }

            if (HasCreateExporterButton(meshBehaviour) && GUILayout.Button(Tr("Create Exporter")))
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