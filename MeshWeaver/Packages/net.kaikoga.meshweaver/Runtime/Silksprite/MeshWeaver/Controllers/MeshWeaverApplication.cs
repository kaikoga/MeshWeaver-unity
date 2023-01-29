using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers
{
    internal static class MeshWeaverApplication
    {
        public static bool IsSelected(GameObject obj)
        {
#if UNITY_EDITOR
            return UnityEditor.Selection.Contains(obj);
#else
            return false;
#endif
        }
    }
}