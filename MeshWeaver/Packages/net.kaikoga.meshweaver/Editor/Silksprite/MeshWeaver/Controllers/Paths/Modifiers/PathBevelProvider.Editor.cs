using Silksprite.MeshWeaver.Controllers.Base;
using UnityEditor;
using UnityEngine.UIElements;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Paths.Modifiers
{
    [CustomEditor(typeof(PathBevelProvider))]
    [CanEditMultipleObjects]
    public class PathBevelProviderEditor : SubProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(VisualElement container)
        {
            container.Add(Prop(nameof(PathBevelProvider.subdivision), Loc("PathBevelProvider.subdivision")));
            container.Add(Prop(nameof(PathBevelProvider.size), Loc("PathBevelProvider.size")));
            container.Add(Prop(nameof(PathBevelProvider.strength), Loc("PathBevelProvider.strength")));
        }
    }
}