using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Meshes.Modifiers;
using UnityEditor;
using UnityEngine.UIElements;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Modifiers
{
    [CustomEditor(typeof(UvChannelRemapperProvider))]
    [CanEditMultipleObjects]
    public class UvChannelRemapperProviderEditor : SubProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(VisualElement container)
        {
            container.Add(Prop(nameof(UvChannelRemapperProvider.uvArea), Loc("UvChannelRemapperProvider.uvArea")));
            container.Add(Prop(nameof(UvChannelRemapperProvider.uvChannel), Loc("UvChannelRemapperProvider.uvChannel")));
        }
    }
}