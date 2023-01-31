using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers
{
#if UNITY_EDITOR
    [UnityEditor.InitializeOnLoad]
#endif
    internal static class MeshWeaverApplication
    {
        public static int globalFrame;

        public static void GlobalUpdate()
        {
            globalFrame++;
        }

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

#if UNITY_EDITOR
        static MeshWeaverApplication()
        {
            UnityEditor.EditorApplication.update += GlobalUpdate;
        }
#endif
    }
}