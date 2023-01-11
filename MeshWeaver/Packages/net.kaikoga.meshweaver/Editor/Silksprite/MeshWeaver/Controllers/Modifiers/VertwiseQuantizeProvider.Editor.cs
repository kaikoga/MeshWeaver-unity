using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Meshes.Modifiers;
using UnityEditor;
using UnityEngine.UIElements;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Modifiers
{
    [CustomEditor(typeof(VertwiseQuantizeProvider))]
    [CanEditMultipleObjects]
    public class VertwiseQuantizeProviderEditor : SubProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(VisualElement container)
        {
            container.Add(Prop(nameof(VertwiseQuantizeProvider.denominator), Loc("VertwiseQuantizeProvider.denominator")));
        }
    }
}