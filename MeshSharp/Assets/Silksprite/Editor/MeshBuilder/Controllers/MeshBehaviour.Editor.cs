using Silksprite.MeshBuilder.Controllers.Utils;
using UnityEditor;
using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers
{
    [CustomEditor(typeof(MeshBehaviour))]
    public class MeshBehaviourEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var meshBehaviour = (MeshBehaviour)target;
            MeshProviderMenus.Menu.PropertyField(meshBehaviour, ref meshBehaviour.meshProviders);
            if (GUILayout.Button("Compile"))
            {
                meshBehaviour.Compile();
            }
        }
    }
}