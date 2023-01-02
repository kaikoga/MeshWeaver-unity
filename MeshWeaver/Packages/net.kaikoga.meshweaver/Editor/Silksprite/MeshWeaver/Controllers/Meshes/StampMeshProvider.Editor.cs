using Silksprite.MeshWeaver.Controllers.Base;
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

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var stampMeshProvider = (StampMeshProvider)target;
            MeshProviderMenus.Menu.PropertyField(stampMeshProvider, "Mesh", ref stampMeshProvider.meshProvider);
            PathProviderMenus.ElementsMenu.PropertyField(stampMeshProvider, "Path", ref stampMeshProvider.pathProvider);

            MeshWeaverGUI.DumpFoldout("Mesh data", ref _isExpandedMesh, () => stampMeshProvider.LastMeshie.Build(MeshWeaverSettings.Current.currentLodMaskLayer));
            MeshWeaverGUI.DumpFoldout("Path data", ref _isExpandedPath, () => stampMeshProvider.LastPathie.Build(MeshWeaverSettings.Current.currentLodMaskLayer));
        }
    }
}