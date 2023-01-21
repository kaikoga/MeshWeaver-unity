using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Utils;
using Silksprite.MeshWeaver.GUIActions;
using UnityEditor;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    [CustomEditor(typeof(StitchMeshProvider))]
    [CanEditMultipleObjects]
    public class StitchMeshProviderEditor : MeshProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(GUIContainer container)
        {
            container.Add(Prop(nameof(StitchMeshProvider.pathProviderA), Loc("StitchMeshProvider.pathProviderA")));
            container.Add(PathProviderMenus.CollectionsMenu.ToGUIAction((StitchMeshProvider)target, "Path A",
                serializedObject.FindProperty(nameof(StitchMeshProvider.pathProviderA))));
            container.Add(Prop(nameof(StitchMeshProvider.pathProviderB), Loc("StitchMeshProvider.pathProviderB")));
            container.Add(PathProviderMenus.CollectionsMenu.ToGUIAction((StitchMeshProvider)target, "Path B",
                serializedObject.FindProperty(nameof(StitchMeshProvider.pathProviderB))));

            container.Add(Prop(nameof(StitchMeshProvider.material), Loc("StitchMeshProvider.material")));
        }

        protected override void PopulateDumpGUI(GUIContainer container)
        {
            container.Add(new DumpFoldout(Loc("Input Path dump A"), () => ((StitchMeshProvider)target).LastPathieA?.Build(MeshWeaverSettings.Current.activeLodMaskLayer)));
            container.Add(new DumpFoldout(Loc("Input Path dump B"), () => ((StitchMeshProvider)target).LastPathieB?.Build(MeshWeaverSettings.Current.activeLodMaskLayer)));
        }
    }
}