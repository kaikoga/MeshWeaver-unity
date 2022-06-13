using UnityEditor;
using UnityEngine;

namespace Silksprite.MeshWeaver.CustomDrawers
{
    [CustomPropertyDrawer(typeof(RectCustomAttribute))]
    public class RectCustomDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            using (new EditorGUI.PropertyScope(position, label, property))
            {
                position = EditorGUI.PrefixLabel(position, label);
                var oldLabelWidth = EditorGUIUtility.labelWidth;
                EditorGUIUtility.labelWidth = 12f;
                EditorGUI.PropertyField(new Rect(position.x, position.y, position.width / 2, position.height / 2), property.FindPropertyRelative("x"));
                EditorGUI.PropertyField(new Rect(position.x + position.width / 2, position.y, position.width / 2, position.height / 2), property.FindPropertyRelative("y"));
                EditorGUI.PropertyField(new Rect(position.x, position.y + position.height / 2, position.width / 2, position.height / 2), property.FindPropertyRelative("width"), new GUIContent("W"));
                EditorGUI.PropertyField(new Rect(position.x + position.width / 2, position.y + position.height / 2, position.width / 2, position.height / 2), property.FindPropertyRelative("height"), new GUIContent("H"));
                EditorGUIUtility.labelWidth = oldLabelWidth;
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 2 + EditorGUIUtility.standardVerticalSpacing;
    }
    }
}