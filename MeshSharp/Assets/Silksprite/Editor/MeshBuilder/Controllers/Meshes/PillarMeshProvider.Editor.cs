using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Controllers.Utils;
using Silksprite.MeshBuilder.Utils;
using UnityEditor;

namespace Silksprite.MeshBuilder.Controllers.Meshes
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
            PathProviderMenus.CollectionsMenu.PropertyField(pillarMeshProvider, ref pillarMeshProvider.pathProviderX);
            PathProviderMenus.CollectionsMenu.PropertyField(pillarMeshProvider, ref pillarMeshProvider.pathProviderY);

            MeshBuilderGUI.DumpFoldout("Path data X", ref _isExpandedX, () => pillarMeshProvider.LastPathieX);
            MeshBuilderGUI.DumpFoldout("Path data Y", ref _isExpandedY, () => pillarMeshProvider.LastPathieY);
        }
    }
}