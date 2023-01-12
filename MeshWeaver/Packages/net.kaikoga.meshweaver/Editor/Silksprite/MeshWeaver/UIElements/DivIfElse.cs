using Silksprite.MeshWeaver.UIElements.Extensions;
using UnityEngine.UIElements;

namespace Silksprite.MeshWeaver.UIElements
{
    public class DivIfElse : VisualElement
    {
        readonly VisualElement _ifContent;
        readonly VisualElement _elseContent;
        bool _value;

        public DivIfElse WithDisplay(bool displayIfTrue)
        {
            _ifContent.WithDisplay(displayIfTrue);
            _elseContent.WithDisplay(!displayIfTrue);
            return this;
        }

        public DivIfElse(VisualElement ifContent) : this(ifContent, new VisualElement()) { }

        public DivIfElse(VisualElement ifContent, VisualElement elseContent)
        {
            _ifContent = ifContent;
            _elseContent = elseContent;
            Add(_ifContent);
            Add(_elseContent);
            WithDisplay(false);
        }
    }
}