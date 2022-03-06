using UnityEditor;

namespace Silksprite.MeshBuilder.Controllers.Base
{
    [CustomEditor(typeof(PathProvider), true, isFallback = true)]
    public class PathProviderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            using (new EditorGUI.DisabledScope(false))
            {
                EditorGUILayout.TextArea(((PathProvider)target).LastPathie?.ToString() ?? "null");
            }
        }
    }
}