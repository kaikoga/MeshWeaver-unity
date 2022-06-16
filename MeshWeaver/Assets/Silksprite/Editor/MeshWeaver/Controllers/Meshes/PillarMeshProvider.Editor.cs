using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Utils;
using Silksprite.MeshWeaver.Utils;
using UnityEditor;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    [CustomEditor(typeof(PillarMeshProvider))]
    [CanEditMultipleObjects]
    public class PillarMeshProviderEditor : MeshProviderEditor
    {
        bool _isExpandedX;
        bool _isExpandedY;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var pillarMeshProvider = (PillarMeshProvider)target;
            PathProviderMenus.CollectionsMenu.PropertyField(pillarMeshProvider, "Path X", ref pillarMeshProvider.pathProviderX);
            PathProviderMenus.CollectionsMenu.PropertyField(pillarMeshProvider, "Path Y", ref pillarMeshProvider.pathProviderY);

            MeshWeaverGUI.DumpFoldout("Path data X", ref _isExpandedX, () => pillarMeshProvider.LastPathieX.Build(GuessCurrentLodMaskLayer()));
            MeshWeaverGUI.DumpFoldout("Path data Y", ref _isExpandedY, () => pillarMeshProvider.LastPathieY.Build(GuessCurrentLodMaskLayer()));
        }
    }
}