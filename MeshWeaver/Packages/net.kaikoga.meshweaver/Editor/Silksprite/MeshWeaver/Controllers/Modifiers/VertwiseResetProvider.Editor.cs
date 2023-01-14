using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.GUIActions;
using UnityEditor;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Modifiers
{
    [CustomEditor(typeof(VertwiseResetProvider))]
    [CanEditMultipleObjects]
    public class VertwiseResetProviderEditor : SubProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(GUIContainer container)
        {
            container.Add(Prop(nameof(VertwiseResetProvider.resetPosition), Loc("VertwiseResetProvider.resetPosition")));
            container.Add(Prop(nameof(VertwiseResetProvider.resetRotation), Loc("VertwiseResetProvider.resetRotation")));
            container.Add(Prop(nameof(VertwiseResetProvider.resetScale), Loc("VertwiseResetProvider.resetScale")));
        }
    }
}