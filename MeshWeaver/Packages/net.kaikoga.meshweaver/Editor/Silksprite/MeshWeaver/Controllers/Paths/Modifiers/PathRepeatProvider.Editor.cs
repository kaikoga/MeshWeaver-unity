using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Utils;
using UnityEditor;
using UnityEngine.UIElements;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Paths.Modifiers
{
    [CustomEditor(typeof(PathRepeatProvider))]
    [CanEditMultipleObjects]
    public class PathRepeatProviderEditor : SubProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(VisualElement container)
        {
            container.Add(Prop(nameof(PathRepeatProvider.count), Loc("PathRepeatProvider.count")));
            container.Add(Prop(nameof(PathRepeatProvider.offset), Loc("PathRepeatProvider.offset")));
            container.Add(Prop(nameof(PathRepeatProvider.offsetByReference), Loc("PathRepeatProvider.offsetByReference")));
            container.Add(TransformMenus.Menu.VisualElement((PathRepeatProvider)target, "Offset By Reference",
                serializedObject.FindProperty(nameof(PathRepeatProvider.offsetByReference))));
            container.Add(Prop(nameof(PathRepeatProvider.offsetByPath), Loc("PathRepeatProvider.offsetByPath")));
            container.Add(Prop(nameof(PathRepeatProvider.smoothJoin), Loc("PathRepeatProvider.smoothJoin")));
        }
    }
}