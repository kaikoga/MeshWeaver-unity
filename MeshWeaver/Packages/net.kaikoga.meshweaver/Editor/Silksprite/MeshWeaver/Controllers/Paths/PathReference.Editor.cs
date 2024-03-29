using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.GUIActions;
using UnityEditor;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Paths
{
    [CustomEditor(typeof(PathReference))]
    [CanEditMultipleObjects]
    public class PathReferenceEditor : PathProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(GUIContainer container)
        {
            container.Add(Prop(nameof(PathReference.pathProviders), Loc("PathReference.pathProviders")));
            container.Add(Prop(nameof(PathReference.isLoop), Loc("PathReference.isLoop")));
            container.Add(Prop(nameof(PathReference.smoothJoin), Loc("PathReference.smoothJoin")));
        }
    }
}