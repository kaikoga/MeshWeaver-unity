using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Utils;
using UnityEditor;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    [CustomEditor(typeof(PolygonMeshProvider))]
    [CanEditMultipleObjects]
    public class PolygonMeshProviderEditor : MeshProviderEditor
    {
        protected override void OnPropertiesGUI()
        {
            base.OnPropertiesGUI();
            var polygonMeshProvider = (PolygonMeshProvider)target;
            PathProviderMenus.CollectionsMenu.PropertyField(polygonMeshProvider, "Path", "Path", ref polygonMeshProvider.pathProvider);
        }
    }
}