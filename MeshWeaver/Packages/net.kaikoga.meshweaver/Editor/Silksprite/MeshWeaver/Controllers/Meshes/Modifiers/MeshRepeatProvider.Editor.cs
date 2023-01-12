using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Utils;
using UnityEditor;
using UnityEngine.UIElements;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Meshes.Modifiers
{
    [CustomEditor(typeof(MeshRepeatProvider))]
    [CanEditMultipleObjects]
    public class MeshRepeatProviderEditor : SubProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(VisualElement container)
        {
            container.Add(Prop(nameof(MeshRepeatProvider.count), Loc("MeshRepeatProvider.count")));
            container.Add(Prop(nameof(MeshRepeatProvider.offset), Loc("MeshRepeatProvider.offset")));
            container.Add(Prop(nameof(MeshRepeatProvider.offsetByReference), Loc("MeshRepeatProvider.offsetByReference")));
            container.Add(TransformMenus.Menu.VisualElement((MeshRepeatProvider)target, "Offset By Reference",
                serializedObject.FindProperty(nameof(MeshRepeatProvider.offsetByReference))));
        }
    }
}