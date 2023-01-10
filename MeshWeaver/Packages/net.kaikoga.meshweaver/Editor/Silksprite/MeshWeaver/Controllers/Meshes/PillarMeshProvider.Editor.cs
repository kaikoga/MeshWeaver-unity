using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Fallback;
using Silksprite.MeshWeaver.Controllers.Utils;
using Silksprite.MeshWeaver.Utils;
using UnityEditor;
using UnityEngine.UIElements;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    [CustomEditor(typeof(PillarMeshProvider))]
    [CanEditMultipleObjects]
    public class PillarMeshProviderEditor : MeshProviderEditor
    {
        bool _isExpandedX;
        bool _isExpandedY;

        protected override void PopulatePropertiesGUI(VisualElement container)
        {
            container.Add(new IMGUIContainer(() =>
            {
                var pillarMeshProvider = (PillarMeshProvider)target;
                PathProviderMenus.CollectionsMenu.PropertyField(pillarMeshProvider, "Path X", "Path X", ref pillarMeshProvider.pathProviderX);
                PathProviderMenus.CollectionsMenu.PropertyField(pillarMeshProvider, "Path Y", "Path Y", ref pillarMeshProvider.pathProviderY);
            }));
        }

        protected override void PopulateDumpGUI(VisualElement container)
        {
            container.Add(new IMGUIContainer(() =>
            {
                var pillarMeshProvider = (PillarMeshProvider)target;
                MeshWeaverGUI.DumpFoldout("Path data X", ref _isExpandedX, () => pillarMeshProvider.LastPathieX?.Build(MeshWeaverSettings.Current.CurrentLodMaskLayer));
                MeshWeaverGUI.DumpFoldout("Path data Y", ref _isExpandedY, () => pillarMeshProvider.LastPathieY?.Build(MeshWeaverSettings.Current.CurrentLodMaskLayer));
            }));
        }
    }
}