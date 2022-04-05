using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Utils;
using UnityEditor;

namespace Silksprite.MeshWeaver.Controllers.Meshes
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