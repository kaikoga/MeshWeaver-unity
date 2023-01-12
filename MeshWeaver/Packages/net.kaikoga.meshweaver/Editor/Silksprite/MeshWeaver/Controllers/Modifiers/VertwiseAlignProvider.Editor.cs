using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Meshes.Modifiers;
using UnityEditor;
using UnityEngine.UIElements;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Modifiers
{
    [CustomEditor(typeof(VertwiseAlignProvider))]
    [CanEditMultipleObjects]
    public class VertwiseAlignProviderEditor : SubProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(VisualElement container)
        {
            container.Add(Prop(nameof(VertwiseAlignProvider.alignPosition), Loc("VertwiseAlignProvider.alignPosition")));
            container.Add(Prop(nameof(VertwiseAlignProvider.alignRotation), Loc("VertwiseAlignProvider.alignRotation")));
            container.Add(Prop(nameof(VertwiseAlignProvider.alignScale), Loc("VertwiseAlignProvider.alignScale")));
        }
    }
}