using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Meshes.Modifiers;
using Silksprite.MeshWeaver.GUIActions;
using UnityEditor;
using UnityEngine.UIElements;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Modifiers
{
    [CustomEditor(typeof(UvRandomizerProvider))]
    [CanEditMultipleObjects]
    public class UvRandomizerProviderEditor : SubProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(GUIContainer container)
        {
            container.Add(Prop(nameof(UvRandomizerProvider.@base), Loc("UvRandomizerProvider.@base")));
            container.Add(Prop(nameof(UvRandomizerProvider.range), Loc("UvRandomizerProvider.range")));
            container.Add(Prop(nameof(UvRandomizerProvider.seed), Loc("UvRandomizerProvider.seed")));
        }
    }
}