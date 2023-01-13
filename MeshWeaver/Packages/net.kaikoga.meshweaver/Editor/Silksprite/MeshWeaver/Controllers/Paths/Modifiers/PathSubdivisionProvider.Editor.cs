using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.GUIActions;
using UnityEditor;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Paths.Modifiers
{
    [CustomEditor(typeof(PathSubdivisionProvider))]
    [CanEditMultipleObjects]
    public class PathSubdivisionProviderEditor : SubProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(GUIContainer container)
        {
            container.Add(Prop(nameof(PathSubdivisionProvider.maxCount), Loc("PathSubdivisionProvider.maxCount")));
            container.Add(Prop(nameof(PathSubdivisionProvider.maxLength), Loc("PathSubdivisionProvider.maxLength")));
        }
    }
}