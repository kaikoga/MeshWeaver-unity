using UnityEditor;

namespace Silksprite.MeshWeaver.Controllers
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(MeshBehaviourProfile))]
    public class MeshBehaviourProfileEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var serializedObj = serializedObject;
            var property = serializedObject.FindProperty("data");
            property.NextVisible(true);
            do
            {
                EditorGUILayout.PropertyField(property);
            } while (property.NextVisible(false));
            serializedObj.ApplyModifiedProperties();
        }
    }
}