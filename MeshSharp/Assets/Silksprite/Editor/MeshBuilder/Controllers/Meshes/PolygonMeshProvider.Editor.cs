using Silksprite.MeshBuilder.Controllers.Paths;
using Silksprite.MeshBuilder.Utils;
using UnityEditor;

namespace Silksprite.MeshBuilder.Controllers.Meshes
{
    [CustomEditor(typeof(PolygonMeshProvider))]
    public class PolygonMeshProviderEditor : Editor
    {
        static readonly ComponentPopupMenu<PathProvider> PathProviderMenu = new ComponentPopupMenu<PathProvider>
        (
            typeof(CompositePathProvider),
            typeof(FixedPathProvider)
        );

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var polygonMeshProvider = (PolygonMeshProvider)target;
            var child = PathProviderMenu.ChildPopup(polygonMeshProvider.transform);
            if (child != null)
            {
                polygonMeshProvider.pathProviders.Add(child);
            }
        }
    }
}