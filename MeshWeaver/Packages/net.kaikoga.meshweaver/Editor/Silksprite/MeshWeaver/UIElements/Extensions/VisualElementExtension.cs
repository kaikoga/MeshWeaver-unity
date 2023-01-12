using System;
using UnityEngine.UIElements;

namespace Silksprite.MeshWeaver.UIElements.Extensions
{
    public static class VisualElementExtension
    {
        public static T WithDisplay<T>(this T visualElement, bool displayIfTrue)
            where T : VisualElement
        {
            visualElement.style.display = new StyleEnum<DisplayStyle>(displayIfTrue ? DisplayStyle.Flex : DisplayStyle.None);
            return visualElement;
        }

        public static T WithEnabled<T>(this T visualElement, bool enableIfTrue)
            where T : VisualElement
        {
            visualElement.SetEnabled(enableIfTrue);
            return visualElement;
        }

        public static T WithDisplayOnRefresh<T>(this T visualElement, Dispatcher<RefreshEvent> onRefresh, Func<bool> displayIfTrue)
            where T : VisualElement
        {
            onRefresh.Add(_ => visualElement.WithDisplay(displayIfTrue()));
            return visualElement;
        }

        public static DivIfElse WithDisplayOnRefresh(this DivIfElse visualElement, Dispatcher<RefreshEvent> onRefresh, Func<bool> displayIfTrue)
        {
            onRefresh.Add(_ => visualElement.WithDisplay(displayIfTrue()));
            return visualElement;
        }

        public static T WithEnableOnRefresh<T>(this T visualElement, Dispatcher<RefreshEvent> onRefresh, Func<bool> enableIfTrue)
            where T : VisualElement
        {
            onRefresh.Add(_ => visualElement.WithEnabled(enableIfTrue()));
            return visualElement;
        }
    }
}