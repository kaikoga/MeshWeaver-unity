using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Meshes.Modifiers;
using UnityEditor;
using UnityEngine.UIElements;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Modifiers
{
    [CustomEditor(typeof(VertwiseNormalizeProvider))]
    [CanEditMultipleObjects]
    public class VertwiseNormalizeProviderEditor : SubProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(VisualElement container)
        {
            container.Add(Prop(nameof(VertwiseNormalizeProvider.bounds), Loc("VertwiseNormalizeProvider.bounds")));
        }
    }
}