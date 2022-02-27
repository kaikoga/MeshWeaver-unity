using Silksprite.MeshBuilder.Controllers.Paths;
using Silksprite.MeshBuilder.Utils;
using UnityEditor;

namespace Silksprite.MeshBuilder.Controllers.Meshes
{
    [CustomEditor(typeof(MatrixMeshProvider))]
    public class MatrixMeshProviderEditor : Editor
    {
        static readonly ComponentPopupMenu<PathProvider> PathProviderMenu = new ComponentPopupMenu<PathProvider>
        (
            typeof(CompositePathProvider),
            typeof(FixedPathProvider)
        );

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var matrixMeshProvider = (MatrixMeshProvider)target;
            var child = PathProviderMenu.ChildPopup(matrixMeshProvider.transform);
            if (child != null)
            {
                matrixMeshProvider.pathProvidersX.Add(child);
            }
            child = PathProviderMenu.ChildPopup(matrixMeshProvider.transform);
            if (child != null)
            {
                matrixMeshProvider.pathProvidersY.Add(child);
            }
        }
    }
}