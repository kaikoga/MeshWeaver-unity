using System;
using UnityEngine.UIElements;

namespace Silksprite.MeshWeaver.UIElements
{
    public class Div : VisualElement
    {
        public Div(Action<VisualElement> initializer) : this(null, initializer) { }

        public Div(string containerName, Action<VisualElement> initializer)
        {
            name = containerName;
            initializer(this);
        }
    }
}