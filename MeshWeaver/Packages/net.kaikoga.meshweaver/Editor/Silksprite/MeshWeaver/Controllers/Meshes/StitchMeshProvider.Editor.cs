using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Utils;
using Silksprite.MeshWeaver.UIElements;
using UnityEditor;
using UnityEngine.UIElements;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    [CustomEditor(typeof(StitchMeshProvider))]
    [CanEditMultipleObjects]
    public class StitchMeshProviderEditor : MeshProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(VisualElement container)
        {
            container.Add(Prop(nameof(StitchMeshProvider.pathProviderA), Loc("StitchMeshProvider.pathProviderA")));
            container.Add(PathProviderMenus.CollectionsMenu.VisualElement((StitchMeshProvider)target, "Path A",
                serializedObject.FindProperty(nameof(StitchMeshProvider.pathProviderA))));
            container.Add(Prop(nameof(StitchMeshProvider.pathProviderB), Loc("StitchMeshProvider.pathProviderB")));
            container.Add(PathProviderMenus.CollectionsMenu.VisualElement((StitchMeshProvider)target, "Path B",
                serializedObject.FindProperty(nameof(StitchMeshProvider.pathProviderB))));

            container.Add(Prop(nameof(StitchMeshProvider.material), Loc("StitchMeshProvider.material")));
        }

        protected override void PopulateDumpGUI(VisualElement container)
        {
            container.Add(new DumpFoldout(Loc("Path data A"), () => ((StitchMeshProvider)target).LastPathieA?.Build(MeshWeaverSettings.Current.CurrentLodMaskLayer)));
            container.Add(new DumpFoldout(Loc("Path data B"), () => ((StitchMeshProvider)target).LastPathieB?.Build(MeshWeaverSettings.Current.CurrentLodMaskLayer)));
        }
    }
}