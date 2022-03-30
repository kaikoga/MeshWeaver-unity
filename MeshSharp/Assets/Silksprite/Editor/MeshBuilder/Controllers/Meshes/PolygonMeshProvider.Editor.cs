using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Controllers.Utils;
using UnityEditor;

namespace Silksprite.MeshBuilder.Controllers.Meshes
{
    [CustomEditor(typeof(PolygonMeshProvider))]
    [CanEditMultipleObjects]
    public class PolygonMeshProviderEditor : MeshProviderEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var polygonMeshProvider = (PolygonMeshProvider)target;
            PathProviderMenus.CollectionsMenu.PropertyField(polygonMeshProvider, "Path", ref polygonMeshProvider.pathProvider);
        }
    }
}