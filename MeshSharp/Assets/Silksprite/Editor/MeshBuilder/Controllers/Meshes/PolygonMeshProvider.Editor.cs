using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Controllers.Paths;
using Silksprite.MeshBuilder.Utils;
using UnityEditor;

namespace Silksprite.MeshBuilder.Controllers.Meshes
{
    [CustomEditor(typeof(PolygonMeshProvider))]
    public class PolygonMeshProviderEditor : Editor
    {
        static readonly ChildComponentPopupMenu<PathProvider> PathProviderMenu = new ChildComponentPopupMenu<PathProvider>
        (
            typeof(CompositePathProvider),
            typeof(PathReference),
            typeof(FixedPathProvider)
        );

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var polygonMeshProvider = (PolygonMeshProvider)target;
            PathProviderMenu.PropertyField(polygonMeshProvider, ref polygonMeshProvider.pathProvider);
        }
    }
}