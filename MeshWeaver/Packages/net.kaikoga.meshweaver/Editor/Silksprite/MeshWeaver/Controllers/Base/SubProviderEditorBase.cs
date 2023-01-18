using System.Collections.Generic;
using Silksprite.MeshWeaver.GUIActions;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Base
{
    public abstract class SubProviderEditorBase : ProviderEditorBase
    {
        protected override bool IsMainComponentEditor => false;

        protected sealed override void PopulateInspectorGUI(GUIContainer container)
        {
            container.Add(CreatePropertiesGUI());
            container.Add(CreateAdvancedActionsGUI());
        }

        GUIAction CreatePropertiesGUI()
        {
            return new Div(c =>
            {
                c.Add(Prop(nameof(MeshProvider.lodMask), Loc("ModifierProviderBase.lodMask")));
                PopulatePropertiesGUI(c);
            });
        }
        
        GUIAction CreateAdvancedActionsGUI()
        {
            var menuItems = new List<LocMenuItem>();
            PopulateAdvancedActions(menuItems);
            if (menuItems.Count == 0) return new GUIContainer();
            return new LocPopupButtons(Loc("Advanced Actions"), Loc("Command..."), menuItems.ToArray());
        }
        
        protected abstract void PopulatePropertiesGUI(GUIContainer container);

        protected virtual void PopulateAdvancedActions(List<LocMenuItem> menuItems)
        {
        }
    }
}