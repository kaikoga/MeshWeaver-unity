using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Fallback;
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

        protected override void OnPropertiesGUI()
        {
            base.OnPropertiesGUI();
            var matrixMeshProvider = (MatrixMeshProvider)target;
            PathProviderMenus.CollectionsMenu.PropertyField(matrixMeshProvider, "Path X", "Path X", ref matrixMeshProvider.pathProviderX);
            PathProviderMenus.CollectionsMenu.PropertyField(matrixMeshProvider, "Path Y", "Path Y", ref matrixMeshProvider.pathProviderY);
        }

        protected override void OnDumpGUI()
        {
            base.OnDumpGUI();
            var matrixMeshProvider = (MatrixMeshProvider)target;
            MeshWeaverGUI.DumpFoldout("Path data X", ref _isExpandedX, () => matrixMeshProvider.LastPathieX.Build(MeshWeaverSettings.Current.currentLodMaskLayer));
            MeshWeaverGUI.DumpFoldout("Path data Y", ref _isExpandedY, () => matrixMeshProvider.LastPathieY.Build(MeshWeaverSettings.Current.currentLodMaskLayer));
        }
    }
}