using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Controllers.Meshes;
using Silksprite.MeshBuilder.Utils;
using UnityEditor;
using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers
{
    [CustomEditor(typeof(MeshBehaviour))]
    public class MeshBehaviourEditor : Editor
    {
        static readonly ChildComponentPopupMenu<MeshProvider> MeshProviderMenu = new ChildComponentPopupMenu<MeshProvider>(
            typeof(PolygonMeshProvider),
            typeof(MatrixMeshProvider)
        );

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var meshBehaviour = (MeshBehaviour)target;
            MeshProviderMenu.PropertyField(meshBehaviour, ref meshBehaviour.meshProviders);
            if (GUILayout.Button("Compile"))
            {
                meshBehaviour.Compile();
            }
        }
    }
}