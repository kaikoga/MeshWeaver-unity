using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Utils;
using Silksprite.MeshWeaver.Utils;
using UnityEditor;

namespace Silksprite.MeshWeaver.Controllers.Meshes
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

            MeshWeaverGUI.DumpFoldout("Path data X", ref _isExpandedX, () => matrixMeshProvider.LastPathieX.Build(GuessCurrentLodMaskLayer()));
            MeshWeaverGUI.DumpFoldout("Path data Y", ref _isExpandedY, () => matrixMeshProvider.LastPathieY.Build(GuessCurrentLodMaskLayer()));
        }
    }
}