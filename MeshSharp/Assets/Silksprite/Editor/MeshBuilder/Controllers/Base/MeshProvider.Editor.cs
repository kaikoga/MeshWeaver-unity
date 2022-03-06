using UnityEditor;

namespace Silksprite.MeshBuilder.Controllers.Base
{
    [CustomEditor(typeof(MeshProvider), true, isFallback = true)]
    public class MeshProviderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            using (new EditorGUI.DisabledScope(false))
            {
                EditorGUILayout.TextArea(((MeshProvider)target).LastMeshie?.ToString() ?? "null");
            }
        }
    }
}