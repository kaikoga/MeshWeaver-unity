using System;

namespace Silksprite.MeshWeaver.GUIActions
{
    public class Div : GUIContainer
    {
        public Div(Action<GUIContainer> initializer) : this(null, initializer) { }

        public Div(string containerName, Action<GUIContainer> initializer)
        {
            initializer(this);
        }
    }
}