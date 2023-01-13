using System;
using Silksprite.MeshWeaver.UIElements;
using UnityEditor;
using RefreshEvent = Silksprite.MeshWeaver.GUIActions.Events.RefreshEvent;

namespace Silksprite.MeshWeaver.GUIActions.Extensions
{
    public static class GUIElementExtension
    {
        public static GUIAction WithDisplay(this GUIAction guiAction, bool displayIfTrue) => WithDisplayImpl(guiAction, () => displayIfTrue);

        public static GUIAction WithEnabled(this GUIAction guiAction, bool enableIfTrue) => WithEnabledImpl(guiAction, () => enableIfTrue);

        public static GUIAction WithDisplayOnRefresh(this GUIAction guiAction, Events.Dispatcher<RefreshEvent> onRefresh, Func<bool> displayIfTrue)
        {
            return WithDisplayImpl(guiAction, displayIfTrue);
        }

        public static GUIAction WithDisplayOnRefresh(this DivIfElse guiElement, Events.Dispatcher<RefreshEvent> onRefresh, Func<bool> displayIfTrue)
        {
            return WithDisplayImpl(guiElement, displayIfTrue);
        }

        public static GUIAction WithEnableOnRefresh(this GUIAction guiAction, Events.Dispatcher<RefreshEvent> onRefresh, Func<bool> enableIfTrue)
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