using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Utils;
using Silksprite.MeshWeaver.GUIActions;
using UnityEditor;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    [CustomEditor(typeof(StampMeshProvider))]
    [CanEditMultipleObjects]
    public class StampMeshProviderEditor : MeshProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(GUIContainer container)
        {
            container.Add(Prop(nameof(StampMeshProvider.meshProvider), Loc("StampMeshProvider.meshProvider")));
            container.Add(MeshProviderMenus.Menu.ToGUIAction((StampMeshProvider)target, "Mesh",
                serializedObject.FindProperty(nameof(StampMeshProvider.meshProvider))));
            container.Add(Prop(nameof(StampMeshProvider.pathProvider), Loc("StampMeshProvider.pathProvider")));
            container.Add(PathProviderMenus.ElementsMenu.ToGUIAction((StampMeshProvider)target, "Path",
                serializedObject.FindProperty(nameof(StampMeshProvider.pathProvider))));
        }

        protected override void PopulateDumpGUI(GUIContainer container)
        {
            container.Add(new DumpFoldout(Loc("Input Mesh dump"), () => ((StampMeshProvider)target).LastMeshie?.Build(MeshWeaverSettings.Current.CurrentLodMaskLayer)));
            container.Add(new DumpFoldout(Loc("Input Path dump"), () => ((StampMeshProvider)target).LastPathie?.Build(MeshWeaverSettings.Current.CurrentLodMaskLayer)));
        }
    }
}