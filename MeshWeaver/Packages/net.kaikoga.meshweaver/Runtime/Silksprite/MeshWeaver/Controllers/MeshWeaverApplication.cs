using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers
{
    internal static class MeshWeaverApplication
    {
        public static int EmitRevision()
        {
            return Random.Range(0, 0x7fffffff);
        }

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