using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Controllers.Utils;
using UnityEditor;

namespace Silksprite.MeshBuilder.Controllers.Meshes
{
    [CustomEditor(typeof(PolygonMeshProvider))]
    public class PolygonMeshProviderEditor : MeshProviderEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var polygonMeshProvider = (PolygonMeshProvider)target;
            PathProviderMenus.CollectionsMenu.PropertyField(polygonMeshProvider, ref polygonMeshProvider.pathProvider);
        }
    }
}