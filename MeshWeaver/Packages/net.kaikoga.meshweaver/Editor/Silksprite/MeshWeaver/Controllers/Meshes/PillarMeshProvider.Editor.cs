using Silksprite.MeshWeaver.Controllers.Fallback;
using Silksprite.MeshWeaver.Controllers.Utils;
using Silksprite.MeshWeaver.UIElements;
using UnityEditor;
using UnityEngine.UIElements;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    [CustomEditor(typeof(PillarMeshProvider))]
    [CanEditMultipleObjects]
    public class PillarMeshProviderEditor : MeshProviderEditor
    {
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
            var pillarMeshProvider = (PillarMeshProvider)target;
            container.Add(new DumpFoldout(Loc("Path data X"), () => pillarMeshProvider.LastPathieX?.Build(MeshWeaverSettings.Current.CurrentLodMaskLayer)));
            container.Add(new DumpFoldout(Loc("Path data Y"), () => pillarMeshProvider.LastPathieY?.Build(MeshWeaverSettings.Current.CurrentLodMaskLayer)));
        }
    }
}