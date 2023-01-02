using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Utils;
using Silksprite.MeshWeaver.Utils;
using UnityEditor;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    [CustomEditor(typeof(StitchMeshProvider))]
    [CanEditMultipleObjects]
    public class StitchMeshProviderEditor : MeshProviderEditor
    {
        bool _isExpandedA;
        bool _isExpandedB;

        protected override void OnPropertiesGUI()
        {
            base.OnPropertiesGUI();
            var stitchMeshProvider = (StitchMeshProvider)target;
            PathProviderMenus.CollectionsMenu.PropertyField(stitchMeshProvider, "Path A", "Path A", ref stitchMeshProvider.pathProviderA);
            PathProviderMenus.CollectionsMenu.PropertyField(stitchMeshProvider, "Path B", "Path B", ref stitchMeshProvider.pathProviderB);
        }

        protected override void OnDumpGUI()
        {
            base.OnDumpGUI();
            var stitchMeshProvider = (StitchMeshProvider)target;
            MeshWeaverGUI.DumpFoldout("Path data A", ref _isExpandedA, () => stitchMeshProvider.LastPathieA.Build(MeshWeaverSettings.Current.currentLodMaskLayer)?.Dump());
            MeshWeaverGUI.DumpFoldout("Path data B", ref _isExpandedB, () => stitchMeshProvider.LastPathieB.Build(MeshWeaverSettings.Current.currentLodMaskLayer)?.Dump());
        }
    }
}