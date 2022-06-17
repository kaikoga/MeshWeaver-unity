using Silksprite.MeshWeaver.Utils;
using UnityEditor;
using UnityEngine;

namespace Silksprite.MeshWeaver.CustomDrawers
{
    [CustomPropertyDrawer(typeof(RectCustomAttribute))]
    public class RectCustomDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var preferMinMax = ((RectCustomAttribute)attribute).preferMinMax;

            using (new EditorGUI.PropertyScope(position, label, property))
            {
                position = EditorGUI.PrefixLabel(position, label);
                using (new EditorGUI.IndentLevelScope(-EditorGUI.indentLevel))
                {
                    var firstLine = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
                    var secondLine = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing, position.width, EditorGUIUtility.singleLineHeight);

                    var minMax = property.isExpanded != preferMinMax;
                    using (var changeCheck = new EditorGUI.ChangeCheckScope())
                    {
                        minMax = EditorGUI.ToggleLeft(new Rect(position.xMax - 70f, position.y, 70f, EditorGUIUtility.singleLineHeight), "MinMax", minMax);
                        if (changeCheck.changed) property.isExpanded = minMax != preferMinMax;
                    }
                    if (minMax)
                    {
                        GUI.Label(new Rect(firstLine.x - 40f, firstLine.y, 40f, firstLine.height), new GUIContent("Min"));
                        GUI.Label(new Rect(secondLine.x - 40f, secondLine.y, 40f, secondLine.height), new GUIContent("Max"));

                        var rect = property.rectValue;
                        using (var changeCheck = new EditorGUI.ChangeCheckScope())
                        {
                            MeshWeaverGUI.HorizontalPropertyFields(firstLine, new[] { new GUIContent("X"), new GUIContent("Y") }, 12f,
                                (pos, lbl, i) =>
                                {
                                    switch (i)
                                    {
                                        case 0:
                                            using (new EditorGUI.PropertyScope(pos, null, property.FindPropertyRelative("x")))
                                                rect.xMin = EditorGUI.FloatField(pos, lbl, rect.xMin);
                                            break;
                                        case 1:
                                            using (new EditorGUI.PropertyScope(pos, null, property.FindPropertyRelative("y")))
                                                rect.yMin = EditorGUI.FloatField(pos, lbl, rect.yMin);
                                            break;
                                    }
                                });
                            MeshWeaverGUI.HorizontalPropertyFields(secondLine, new[] { new GUIContent("X"), new GUIContent("Y") }, 12f,
                                (pos, lbl, i) =>
                                {
                                    switch (i)
                                    {
                                        case 0:
                                            using (new EditorGUI.PropertyScope(pos, null, property.FindPropertyRelative("width")))
                                                rect.xMax = EditorGUI.FloatField(pos, lbl, rect.xMax);
                                            break;
                                        case 1:
                                            using (new EditorGUI.PropertyScope(pos, null, property.FindPropertyRelative("height")))
                                                rect.yMax = EditorGUI.FloatField(pos, lbl, rect.yMax);
                                            break;
                                    }
                                });
                            if (changeCheck.changed)
                            {
                                property.rectValue = rect;
                            }
                        }
                    }
                    else
                    {
                        var it = property.Copy();
                        it.Next(true);
                        MeshWeaverGUI.MultiPropertyField(firstLine, new[] { new GUIContent("X"), new GUIContent("Y") }, it, 12f);
                        MeshWeaverGUI.MultiPropertyField(secondLine, new[] { new GUIContent("W"), new GUIContent("H") }, it, 12f);
                    }
                }
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 2 + EditorGUIUtility.standardVerticalSpacing;
    }
    }
}