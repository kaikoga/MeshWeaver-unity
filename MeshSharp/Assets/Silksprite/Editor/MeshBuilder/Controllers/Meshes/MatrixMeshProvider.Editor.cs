using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Controllers.Paths;
using Silksprite.MeshBuilder.Utils;
using UnityEditor;

namespace Silksprite.MeshBuilder.Controllers.Meshes
{
    [CustomEditor(typeof(MatrixMeshProvider))]
    public class MatrixMeshProviderEditor : Editor
    {
        static readonly ChildComponentPopupMenu<PathProvider> PathProviderMenu = new ChildComponentPopupMenu<PathProvider>
        (
            typeof(CompositePathProvider),
            typeof(PathReference),
            typeof(FixedPathProvider),
            typeof(ShapePathProvider)
        );

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var matrixMeshProvider = (MatrixMeshProvider)target;
            PathProviderMenu.PropertyField(matrixMeshProvider, ref matrixMeshProvider.pathProviderX);
            PathProviderMenu.PropertyField(matrixMeshProvider, ref matrixMeshProvider.pathProviderY);
        }
    }
}