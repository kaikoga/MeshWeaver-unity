using System;
using UnityEditor;
using UnityEngine;

namespace Silksprite.MeshWeaver.Utils
{
    public static class MeshWeaverGUI
    {
        public static void DumpFoldout(string title, ref bool isExpanded, Func<object> dump)
        {
            isExpanded = EditorGUILayout.Foldout(isExpanded, title);
            if (isExpanded)
            {
                EditorGUILayout.TextArea(dump()?.ToString() ?? "null");
            }
        }

        public static void MultiPropertyField(Rect position, GUIContent[] subLabels, SerializedProperty valuesIterator, float labelWidth)
        {
            void DrawNext(Rect pos, GUIContent label, int i)
            {
                EditorGUI.PropertyField(pos, valuesIterator, label);
                valuesIterator.Next(false);
            }

            HorizontalPropertyFields(position, subLabels, labelWidth, DrawNext);
        }

        public static void HorizontalPropertyFields(Rect position, GUIContent[] subLabels, float labelWidth, Action<Rect, GUIContent, int> drawNext)
        {
            position.width /= Mathf.Max(subLabels.Length, 3);

            var oldLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = labelWidth;
            for (var i = 0; i < subLabels.Length; i++)
            {
                drawNext(position, subLabels[i], i);
                position.x += position.width;
            }
            EditorGUIUtility.labelWidth = oldLabelWidth;
        }
    }
}