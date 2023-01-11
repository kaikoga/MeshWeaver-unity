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
            container.Add(PathProviderMenus.CollectionsMenu.VisualElement((PillarMeshProvider)target, "Path X", 
                serializedObject.FindProperty(nameof(PillarMeshProvider.pathProviderX))));
            container.Add(PathProviderMenus.CollectionsMenu.VisualElement((PillarMeshProvider)target, "Path Y",
                serializedObject.FindProperty(nameof(PillarMeshProvider.pathProviderX))));
        }

        protected override void PopulateDumpGUI(VisualElement container)
        {
            var pillarMeshProvider = (PillarMeshProvider)target;
            container.Add(new DumpFoldout(Loc("Input Path dump X"), () => pillarMeshProvider.LastPathieX?.Build(MeshWeaverSettings.Current.CurrentLodMaskLayer)));
            container.Add(new DumpFoldout(Loc("Input Path dump Y"), () => pillarMeshProvider.LastPathieY?.Build(MeshWeaverSettings.Current.CurrentLodMaskLayer)));
        }
    }
}