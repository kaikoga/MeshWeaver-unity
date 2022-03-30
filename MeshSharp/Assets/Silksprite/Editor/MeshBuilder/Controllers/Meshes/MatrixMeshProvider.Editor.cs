using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Controllers.Utils;
using Silksprite.MeshBuilder.Utils;
using UnityEditor;

namespace Silksprite.MeshBuilder.Controllers.Meshes
{
    [CustomEditor(typeof(MatrixMeshProvider))]
    [CanEditMultipleObjects]
    public class MatrixMeshProviderEditor : MeshProviderEditor
    {
        bool _isExpandedX;
        bool _isExpandedY;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var matrixMeshProvider = (MatrixMeshProvider)target;
            PathProviderMenus.CollectionsMenu.PropertyField(matrixMeshProvider, "Path X", ref matrixMeshProvider.pathProviderX);
            PathProviderMenus.CollectionsMenu.PropertyField(matrixMeshProvider, "Path Y", ref matrixMeshProvider.pathProviderY);

            MeshBuilderGUI.DumpFoldout("Path data X", ref _isExpandedX, () => matrixMeshProvider.LastPathieX.Build(GuessCurrentLodMaskLayer()));
            MeshBuilderGUI.DumpFoldout("Path data Y", ref _isExpandedY, () => matrixMeshProvider.LastPathieY.Build(GuessCurrentLodMaskLayer()));
        }
    }
}