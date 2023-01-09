using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Fallback;
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

        protected override void OnPropertiesGUI()
        {
            base.OnPropertiesGUI();
            var pillarMeshProvider = (PillarMeshProvider)target;
            PathProviderMenus.CollectionsMenu.PropertyField(pillarMeshProvider, "Path X", "Path X", ref pillarMeshProvider.pathProviderX);
            PathProviderMenus.CollectionsMenu.PropertyField(pillarMeshProvider, "Path Y", "Path Y", ref pillarMeshProvider.pathProviderY);
        }

        protected override void OnDumpGUI()
        {
            var pillarMeshProvider = (PillarMeshProvider)target;
            MeshWeaverGUI.DumpFoldout("Path data X", ref _isExpandedX, () => pillarMeshProvider.LastPathieX?.Build(MeshWeaverSettings.Current.currentLodMaskLayer));
            MeshWeaverGUI.DumpFoldout("Path data Y", ref _isExpandedY, () => pillarMeshProvider.LastPathieY?.Build(MeshWeaverSettings.Current.currentLodMaskLayer));
        }
    }
}