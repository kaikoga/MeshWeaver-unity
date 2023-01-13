using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.GUIActions;
using UnityEditor;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Modifiers
{
    [CustomEditor(typeof(VertwiseNormalizeProvider))]
    [CanEditMultipleObjects]
    public class VertwiseNormalizeProviderEditor : SubProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(GUIContainer container)
        {
            container.Add(Prop(nameof(VertwiseNormalizeProvider.bounds), Loc("VertwiseNormalizeProvider.bounds")));
        }
    }
}