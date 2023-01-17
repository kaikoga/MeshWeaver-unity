using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Utils;
using Silksprite.MeshWeaver.GUIActions;
using UnityEditor;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Paths
{
    [CustomEditor(typeof(CompositePathProvider))]
    [CanEditMultipleObjects]
    public class CompositePathProviderEditor : PathProviderEditorBase
    {
        protected override void PopulatePropertiesGUI(GUIContainer container)
        {
            container.Add(PathProviderMenus.ElementsMenu.ToGUIAction((CompositePathProvider)target, Loc("Path Providers")));
            container.Add(Prop(nameof(CompositePathProvider.isLoop), Loc("CompositePathProvider.isLoop")));
            container.Add(Prop(nameof(CompositePathProvider.smoothJoin), Loc("CompositePathProvider.smoothJoin")));
        }
    }
}