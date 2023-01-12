using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Utils;
using UnityEditor;
using UnityEngine.UIElements;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Paths
{
    [CustomEditor(typeof(CompositePathProvider))]
    [CanEditMultipleObjects]
    public class CompositePathProviderEditor : PathProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(VisualElement container)
        {
            container.Add(PathProviderMenus.ElementsMenu.VisualElement((CompositePathProvider)target, Loc("Path Providers")));
            container.Add(Prop(nameof(PathReference.isLoop), Loc("PathReference.isLoop")));
            container.Add(Prop(nameof(PathReference.smoothJoin), Loc("PathReference.smoothJoin")));
        }
    }
}