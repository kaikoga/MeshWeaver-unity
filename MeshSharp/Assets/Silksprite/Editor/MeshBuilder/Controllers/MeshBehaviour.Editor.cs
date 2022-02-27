using Silksprite.MeshBuilder.Controllers.Meshes;
using Silksprite.MeshBuilder.Utils;
using UnityEditor;
using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers
{
    [CustomEditor(typeof(MeshBehaviour))]
    public class MeshBehaviourEditor : Editor
    {
        static readonly ComponentPopupMenu<MeshProvider> MeshProviderMenu = new ComponentPopupMenu<MeshProvider>(
            typeof(PolygonMeshProvider)
        );

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var meshBehaviour = (MeshBehaviour)target;
            var child = MeshProviderMenu.ChildPopup(meshBehaviour.transform);
            if (child != null)
            {
                meshBehaviour.meshProviders.Add(child);
            }
            if (GUILayout.Button("Compile"))
            {
                meshBehaviour.Compile();
            }
        }
    }
}