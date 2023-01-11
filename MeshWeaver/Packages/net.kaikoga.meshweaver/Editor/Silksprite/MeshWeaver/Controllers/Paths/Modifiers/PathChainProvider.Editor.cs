using Silksprite.MeshWeaver.Controllers.Base;
using UnityEditor;
using UnityEngine.UIElements;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Paths.Modifiers
{
    [CustomEditor(typeof(PathChainProvider))]
    [CanEditMultipleObjects]
    public class PathChainProviderEditor : SubProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(VisualElement container)
        {
            container.Add(Prop(nameof(PathChainProvider.rolling), Loc("PathChainProvider.rolling")));
        }
    }
}