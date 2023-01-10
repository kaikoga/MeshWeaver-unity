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

    }
}