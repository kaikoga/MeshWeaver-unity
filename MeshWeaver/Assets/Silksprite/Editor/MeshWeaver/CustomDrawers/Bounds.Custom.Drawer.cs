using UnityEditor;
using UnityEngine;

namespace Silksprite.MeshWeaver.CustomDrawers
{
    [CustomPropertyDrawer(typeof(BoundsCustomAttribute))]
    public class BoundsCustomDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            using (new EditorGUI.PropertyScope(position, label, property))
            {
                position = EditorGUI.PrefixLabel(position, label);
                var oldLabelWidth = EditorGUIUtility.labelWidth;
                EditorGUIUtility.labelWidth = 12f;
                var center = property.FindPropertyRelative("m_Center");
                EditorGUI.PropertyField(new Rect(position.x, position.y, position.width / 3, position.height / 2), center.FindPropertyRelative("x"));
                EditorGUI.PropertyField(new Rect(position.x + position.width / 3, position.y, position.width / 3, position.height / 2), center.FindPropertyRelative("y"));
                EditorGUI.PropertyField(new Rect(position.x + position.width * 2 / 3, position.y, position.width / 3, position.height / 2), center.FindPropertyRelative("z"));
                var extent = property.FindPropertyRelative("m_Extent");
                EditorGUI.PropertyField(new Rect(position.x, position.y + position.height / 2, position.width / 3, position.height / 2), extent.FindPropertyRelative("x"));
                EditorGUI.PropertyField(new Rect(position.x + position.width / 3, position.y + position.height / 2, position.width / 3, position.height / 2), extent.FindPropertyRelative("y"));
                EditorGUI.PropertyField(new Rect(position.x + position.width * 2 / 3, position.y + position.height / 2, position.width / 3, position.height / 2), extent.FindPropertyRelative("z"));
                EditorGUIUtility.labelWidth = oldLabelWidth;
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 2 + EditorGUIUtility.standardVerticalSpacing;
        }
    }
}