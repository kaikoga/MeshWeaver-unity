using UnityEngine;

namespace Silksprite.MeshWeaver.Utils
{
    public static class MeshWeaverSkin
    {
        public static Color Primary => new Color(0.4f, 0.8f, 0.7f, 1.0f);

        public static readonly GUIStyle Header = new GUIStyle { fontStyle = FontStyle.Bold, alignment = TextAnchor.MiddleLeft };
    }
}