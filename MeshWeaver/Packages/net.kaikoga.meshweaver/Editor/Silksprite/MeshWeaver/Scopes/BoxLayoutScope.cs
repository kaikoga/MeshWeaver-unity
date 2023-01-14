using System;
using UnityEngine;

namespace Silksprite.MeshWeaver.Scopes
{
    public class BoxLayoutScope : IDisposable
    {
        static readonly GUIStyle Style = new GUIStyle
        {
            normal =
            {
                background = Texture2D.whiteTexture
            },
            padding = new RectOffset(2, 2, 2, 2)
        };

        readonly IDisposable _parent;

        public BoxLayoutScope() : this(GUI.backgroundColor) { }

        public BoxLayoutScope(Color color)
        {
            var backgroundColor = GUI.backgroundColor;
            GUI.backgroundColor = color;
            _parent = new GUILayout.VerticalScope(Style);
            GUI.backgroundColor = backgroundColor;
        }

        public void Dispose()
        {
            _parent.Dispose();
        }
    }
}