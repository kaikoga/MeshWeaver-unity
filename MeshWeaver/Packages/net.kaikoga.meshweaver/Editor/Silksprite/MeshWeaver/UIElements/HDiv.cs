using System;
using UnityEngine.UIElements;

namespace Silksprite.MeshWeaver.UIElements
{
    public class HDiv : VisualElement
    {
        public HDiv(Action<VisualElement> initializer) : this(null, initializer) { }

        public HDiv(string containerName, Action<VisualElement> initializer)
        {
            name = containerName;
            style.flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row);
            initializer(this);
        }
    }
}