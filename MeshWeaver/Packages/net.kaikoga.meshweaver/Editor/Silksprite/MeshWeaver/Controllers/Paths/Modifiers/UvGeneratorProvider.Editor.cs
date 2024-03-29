using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.GUIActions;
using UnityEditor;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Paths.Modifiers
{
    [CustomEditor(typeof(UvGeneratorProvider))]
    [CanEditMultipleObjects]
    public class UvGeneratorProviderEditor : SubProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(GUIContainer container)
        {
            container.Add(Prop(nameof(UvGeneratorProvider.uvArea), Loc("UvGeneratorProvider.uvArea")));
            container.Add(Prop(nameof(UvGeneratorProvider.absoluteScale), Loc("UvGeneratorProvider.absoluteScale")));
            container.Add(Prop(nameof(UvGeneratorProvider.topologicalWeight), Loc("UvGeneratorProvider.topologicalWeight")));
            container.Add(Prop(nameof(UvGeneratorProvider.uvChannel), Loc("UvGeneratorProvider.uvChannel")));
        }
    }
}