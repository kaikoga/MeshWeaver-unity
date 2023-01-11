using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Utils;
using Silksprite.MeshWeaver.UIElements;
using UnityEditor;
using UnityEngine.UIElements;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    [CustomEditor(typeof(StampMeshProvider))]
    [CanEditMultipleObjects]
    public class StampMeshProviderEditor : MeshProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(VisualElement container)
        {
            container.Add(Prop(nameof(StampMeshProvider.meshProvider), Loc("StampMeshProvider.meshProvider")));
            container.Add(MeshProviderMenus.Menu.VisualElement((StampMeshProvider)target, "Mesh",
                serializedObject.FindProperty(nameof(StampMeshProvider.meshProvider))));
            container.Add(Prop(nameof(StampMeshProvider.pathProvider), Loc("StampMeshProvider.pathProvider")));
            container.Add(PathProviderMenus.ElementsMenu.VisualElement((StampMeshProvider)target, "Path",
                serializedObject.FindProperty(nameof(StampMeshProvider.pathProvider))));
        }

        protected override void PopulateDumpGUI(VisualElement container)
        {
            container.Add(new DumpFoldout(Loc("Mesh data"), () => ((StampMeshProvider)target).LastMeshie?.Build(MeshWeaverSettings.Current.CurrentLodMaskLayer)));
            container.Add(new DumpFoldout(Loc("Path data"), () => ((StampMeshProvider)target).LastPathie?.Build(MeshWeaverSettings.Current.CurrentLodMaskLayer)));
        }
    }
}