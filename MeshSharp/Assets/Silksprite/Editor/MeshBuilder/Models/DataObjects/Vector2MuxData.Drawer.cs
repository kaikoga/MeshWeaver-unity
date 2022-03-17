using UnityEditor;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models.DataObjects
{
    [CustomPropertyDrawer(typeof(Vector2MuxData))]
    public class Vector2MuxDataDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var oldLabelWidth = EditorGUIUtility.labelWidth;
            GUI.Label(new Rect(position.xMin, position.yMin, oldLabelWidth, position.height), property.displayName);
            position.xMin += oldLabelWidth;

            var min = position.min;
            var size = position.size;
            var r1 = new Rect(min, size * new Vector2(0.2f, 1f));
            var r2 = new Rect(min + size * new Vector2(0.2f, 0f), size * new Vector2(0.8f, 1f));

            using (new EditorGUI.IndentLevelScope(1 - EditorGUI.indentLevel))
            {
                EditorGUI.PropertyField(r1, property.FindPropertyRelative("minIndex"), new GUIContent(""));
                EditorGUI.PropertyField(r2, property.FindPropertyRelative("value"), new GUIContent(""));
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}