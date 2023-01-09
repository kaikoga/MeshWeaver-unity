using UnityEngine.UIElements;

namespace Silksprite.MeshWeaver.UIElements
{
    public class DivIfElse : VisualElement
    {
        readonly VisualElement _ifContent;
        readonly VisualElement _elseContent;
        bool _value;

        public bool value
        {
            get => _value;
            set
            {
                _value = value;
                _ifContent.style.display = new StyleEnum<DisplayStyle>(value ? DisplayStyle.Flex : DisplayStyle.None);
                _elseContent.style.display = new StyleEnum<DisplayStyle>(value ? DisplayStyle.None : DisplayStyle.Flex);
            }
        }

        public DivIfElse(VisualElement ifContent) : this(ifContent, new VisualElement()) { }

        public DivIfElse(VisualElement ifContent, VisualElement elseContent)
        {
            _ifContent = ifContent;
            _elseContent = elseContent;
            Add(_ifContent);
            Add(_elseContent);
            value = false;
        }
    }
}