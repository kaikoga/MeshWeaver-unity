using Silksprite.MeshWeaver.Controllers.Base;
using UnityEditor;
using UnityEngine.UIElements;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Paths
{
    [CustomEditor(typeof(RevolutionPathProvider))]
    [CanEditMultipleObjects]
    public class RevolutionPathProviderEditor : PathProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(VisualElement container)
        {
            container.Add(Prop(nameof(RevolutionPathProvider.min), Loc("RevolutionPathProvider.min")));
            container.Add(Prop(nameof(RevolutionPathProvider.max), Loc("RevolutionPathProvider.max")));
            container.Add(Prop(nameof(RevolutionPathProvider.subdivision), Loc("RevolutionPathProvider.subdivision")));
            container.Add(Prop(nameof(RevolutionPathProvider.axis), Loc("RevolutionPathProvider.axis")));
            container.Add(Prop(nameof(RevolutionPathProvider.isLoop), Loc("RevolutionPathProvider.isLoop")));
            container.Add(Prop(nameof(RevolutionPathProvider.vector), Loc("RevolutionPathProvider.vector")));
        }
    }
}