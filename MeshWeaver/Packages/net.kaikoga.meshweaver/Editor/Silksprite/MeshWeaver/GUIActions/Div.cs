using System;

namespace Silksprite.MeshWeaver.GUIActions
{
    public class Div : GUIContainer
    {
        public Div(Action<GUIContainer> initializer)
        {
            initializer(this);
        }
    }
}