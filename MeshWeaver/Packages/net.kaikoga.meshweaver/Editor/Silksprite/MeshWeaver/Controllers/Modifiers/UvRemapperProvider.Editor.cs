using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Meshes.Modifiers;
using Silksprite.MeshWeaver.GUIActions;
using UnityEditor;
using UnityEngine.UIElements;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Modifiers
{
    [CustomEditor(typeof(UvRemapperProvider))]
    [CanEditMultipleObjects]
    public class UvRemapperProviderEditor : SubProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(GUIContainer container)
        {
            container.Add(Prop(nameof(UvRemapperProvider.uvArea), Loc("UvRemapperProvider.uvArea")));
        }
    }
}