using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Fallback;
using Silksprite.MeshWeaver.Controllers.Utils;
using Silksprite.MeshWeaver.Utils;
using UnityEditor;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    [CustomEditor(typeof(StampMeshProvider))]
    [CanEditMultipleObjects]
    public class StampMeshProviderEditor : MeshProviderEditor
    {
        bool _isExpandedMesh;
        bool _isExpandedPath;

        protected override void OnPropertiesGUI()
        {
            base.OnPropertiesGUI();
            var stampMeshProvider = (StampMeshProvider)target;
            MeshProviderMenus.Menu.PropertyField(stampMeshProvider, "Stamp Mesh", "Mesh", ref stampMeshProvider.meshProvider);
            PathProviderMenus.ElementsMenu.PropertyField(stampMeshProvider, "Stamp Along Path", "Path", ref stampMeshProvider.pathProvider);
        }

        protected override void OnDumpGUI()
        {
            base.OnDumpGUI();
            var stampMeshProvider = (StampMeshProvider)target;
            MeshWeaverGUI.DumpFoldout("Mesh data", ref _isExpandedMesh, () => stampMeshProvider.LastMeshie?.Build(MeshWeaverSettings.Current.currentLodMaskLayer));
            MeshWeaverGUI.DumpFoldout("Path data", ref _isExpandedPath, () => stampMeshProvider.LastPathie?.Build(MeshWeaverSettings.Current.currentLodMaskLayer));
        }
    }
}