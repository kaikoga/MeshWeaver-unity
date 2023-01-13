using System;
using Silksprite.MeshWeaver.GUIActions.Events;
using UnityEditor;

namespace Silksprite.MeshWeaver.GUIActions.Extensions
{
    public static class GUIElementExtension
    {
        // FIXME: implement scopes as proper GUIAction attributes instead of wrapped objects
        public static GUIAction WithDisplay(this GUIAction guiAction, bool displayIfTrue) => WithDisplayImpl(guiAction, () => displayIfTrue);

        public static GUIAction WithEnabled(this GUIAction guiAction, bool enableIfTrue) => WithEnabledImpl(guiAction, () => enableIfTrue);

        public static GUIAction WithDisplayOnRefresh(this GUIAction guiAction, Dispatcher<RefreshEvent> onRefresh, Func<bool> displayIfTrue)
        {
            return WithDisplayImpl(guiAction, displayIfTrue);
        }

        public static GUIAction WithDisplayOnRefresh(this DivIfElse guiElement, Dispatcher<RefreshEvent> onRefresh, Func<bool> displayIfTrue)
        {
            onRefresh.Add(_ => guiElement.value = displayIfTrue());
            return guiElement;
        }

        public static GUIAction WithEnableOnRefresh(this GUIAction guiAction, Dispatcher<RefreshEvent> onRefresh, Func<bool> enableIfTrue)
        {
            return WithEnabledImpl(guiAction, enableIfTrue);
        }
        
        static GUIAction WithDisplayImpl(this GUIAction guiAction, Func<bool> displayIfTrue)
        {
            return GUIAction.Build(() =>
            {
                if (displayIfTrue()) guiAction.OnGUI();
            });
        }

        static GUIAction WithEnabledImpl(this GUIAction guiAction, Func<bool> enableIfTrue)
        {
            return GUIAction.Build(() =>
            {
                using (new EditorGUI.DisabledGroupScope(!enableIfTrue()))
                {
                    guiAction.OnGUI();
                }
            });
        }

    }
}