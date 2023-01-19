using UnityEngine;

namespace Silksprite.MeshWeaver.Utils
{
    public static class MeshWeaverSkin
    {
        public static Color Primary => new Color(0.6f, 0.9f, 0.8f, 1.0f);
        public static Color Dump => new Color(0.6f, 0.6f, 0.6f, 1.0f);

        public static readonly GUIStyle Header = new GUIStyle
        {
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleLeft,
            padding = new RectOffset(0, 0, 2, 0)
        };
    }
}