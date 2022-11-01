using Silksprite.MeshWeaver.Utils;
using UnityEditor;
using UnityEngine;

namespace Silksprite.MeshWeaver.CustomDrawers
{
    [CustomPropertyDrawer(typeof(BoundsCustomAttribute))]
    public class BoundsCustomDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var preferMinMax = ((BoundsCustomAttribute)attribute).preferMinMax;

            using (new EditorGUI.PropertyScope(position, label, property))
            {
                position = EditorGUI.PrefixLabel(position, label);
                using (new EditorGUI.IndentLevelScope(-EditorGUI.indentLevel))
                {
                    var firstLine = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing, position.width, EditorGUIUtility.singleLineHeight);
                    var secondLine = new Rect(position.x, position.y + (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * 2, position.width, EditorGUIUtility.singleLineHeight);

                    var minMax = property.isExpanded != preferMinMax;
                    using (var changeCheck = new EditorGUI.ChangeCheckScope())
                    {
                        minMax = EditorGUI.ToggleLeft(new Rect(position.xMax - 70f, position.y, 70f, EditorGUIUtility.singleLineHeight), "MinMax", minMax);
                        if (changeCheck.changed) property.isExpanded = minMax != preferMinMax;
                    }

                    if (minMax)
                    {
                        GUI.Label(new Rect(firstLine.x - 60f, firstLine.y, 60f, firstLine.height), new GUIContent("Min"));
                        GUI.Label(new Rect(secondLine.x - 60f, secondLine.y, 60f, secondLine.height), new GUIContent("Max"));

                        var bounds = property.boundsValue;
                        var min = bounds.min;
                        var max = bounds.max;
                        using (var changeCheck = new EditorGUI.ChangeCheckScope())
                        {
                            MeshWeaverGUI.HorizontalPropertyFields(firstLine, new[] { new GUIContent("X"), new GUIContent("Y"), new GUIContent("Z") }, 10f,
                                (pos, lbl, i) =>
                                {
                                    switch (i)
                                    {
                                        case 0:
                                            min.x = EditorGUI.FloatField(pos, lbl, min.x);
                                            break;
                                        case 1:
                                            min.y = EditorGUI.FloatField(pos, lbl, min.y);
                                            break;
                                        case 2:
                                            min.z = EditorGUI.FloatField(pos, lbl, min.z);
                                            break;
                                    }
                                });
                            MeshWeaverGUI.HorizontalPropertyFields(secondLine, new[] { new GUIContent("X"), new GUIContent("Y"), new GUIContent("Z") }, 10f,
                                (pos, lbl, i) =>
                                {
                                    switch (i)
                                    {
                                        case 0:
                                            max.x = EditorGUI.FloatField(pos, lbl, max.x);
                                            break;
                                        case 1:
                                            max.y = EditorGUI.FloatField(pos, lbl, max.y);
                                            break;
                                        case 2:
                                            max.z = EditorGUI.FloatField(pos, lbl, max.z);
                                            break;
                                    }
                                });
                            if (changeCheck.changed)
                            {
                                bounds.min = min;
                                bounds.max = max;
                                property.boundsValue = bounds;
                            }
                        }
                    }
                    else
                    {
                        GUI.Label(new Rect(firstLine.x - 60f, firstLine.y, 60f, firstLine.height), new GUIContent("Center"));
                        GUI.Label(new Rect(secondLine.x - 60f, secondLine.y, 60f, secondLine.height), new GUIContent("Extent"));

                        var center = property.FindPropertyRelative("m_Center").Copy();
                        center.Next(true);
                        var extent = property.FindPropertyRelative("m_Extent").Copy();
                        extent.Next(true);
                        MeshWeaverGUI.MultiPropertyField(firstLine, new[] { new GUIContent("X"), new GUIContent("Y"), new GUIContent("Z") }, center, 12f);
                        MeshWeaverGUI.MultiPropertyField(secondLine, new[] { new GUIContent("X"), new GUIContent("Y"), new GUIContent("Z") }, extent, 12f);
                    }
                }
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 3 + EditorGUIUtility.standardVerticalSpacing * 2;
        }
    }
}