using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Meshes.Modifiers;
using Silksprite.MeshWeaver.GUIActions;
using UnityEditor;
using UnityEngine.UIElements;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Modifiers
{
    [CustomEditor(typeof(UvChannelRandomizerProvider))]
    [CanEditMultipleObjects]
    public class UvChannelRandomizerProviderEditor : SubProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(GUIContainer container)
        {
            container.Add(Prop(nameof(UvChannelRandomizerProvider.@base), Loc("UvChannelRandomizerProvider.@base")));
            container.Add(Prop(nameof(UvChannelRandomizerProvider.range), Loc("UvChannelRandomizerProvider.range")));
            container.Add(Prop(nameof(UvChannelRandomizerProvider.seed), Loc("UvChannelRandomizerProvider.seed")));
            container.Add(Prop(nameof(UvChannelRandomizerProvider.uvChannel), Loc("UvChannelRandomizerProvider.uvChannel")));
        }
    }
}