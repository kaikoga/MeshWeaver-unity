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
            if (GUILayout.Button("Create Exporter"))
            {
                if (!meshBehaviour.GetComponent<MeshBehaviourExporter>()) meshBehaviour.gameObject.AddComponent<MeshBehaviourExporter>();
            }
        }
    }
}