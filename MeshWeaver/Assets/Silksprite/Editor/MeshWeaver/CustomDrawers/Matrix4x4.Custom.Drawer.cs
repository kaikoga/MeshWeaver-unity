using Silksprite.MeshWeaver.Extensions;
using UnityEditor;
using UnityEngine;

namespace Silksprite.MeshWeaver.CustomDrawers
{
    [CustomPropertyDrawer(typeof(Matrix4x4CustomAttribute))]
    public class Matrix4x4CustomDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var preferTRS = ((Matrix4x4CustomAttribute)attribute).preferTRS;

            using (new EditorGUI.PropertyScope(position, label, property))
            {
                var matrix4x4 = property.GetMatrix4x4Value();

                EditorGUI.PrefixLabel(position, label);
                using (new EditorGUI.IndentLevelScope())
                {
                    var firstLine = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing, position.width, EditorGUIUtility.singleLineHeight);
                    var secondLine = new Rect(position.x, position.y + (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * 2, position.width, EditorGUIUtility.singleLineHeight);
                    var thirdLine = new Rect(position.x, position.y + (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * 3, position.width, EditorGUIUtility.singleLineHeight);

                    var isTRS = property.isExpanded != preferTRS;
                    using (var changeCheck = new EditorGUI.ChangeCheckScope())
                    {
                        isTRS = EditorGUI.ToggleLeft(new Rect(position.xMax - 70f, position.y, 70f, EditorGUIUtility.singleLineHeight), "TRS", isTRS);
                        if (changeCheck.changed) property.isExpanded = isTRS != preferTRS;
                    }

                    if (GUI.Button(new Rect(position.xMax - 150f, position.y, 70f, EditorGUIUtility.singleLineHeight), "Reset"))
                    {
                        // TODO: Record undo
                        matrix4x4 = Matrix4x4.identity;
                        property.SetMatrix4x4Value(matrix4x4);
                    }

                    if (isTRS)
                    {
                        var t = (Vector3)matrix4x4.GetRow(3);
                        var er = matrix4x4.rotation.eulerAngles;
                        var s = matrix4x4.lossyScale;

                        using (var changeCheck = new EditorGUI.ChangeCheckScope())
                        {
                            t = EditorGUI.Vector3Field(firstLine, "Position", t);
                            er = EditorGUI.Vector3Field(secondLine, "Rotation", er);
                            s = EditorGUI.Vector3Field(thirdLine, "Scale", s);
                            if (changeCheck.changed)
                            {
                                // TODO: Record undo
                                matrix4x4.SetTRS(Vector3.zero, Quaternion.Euler(er), s);
                                matrix4x4.SetRow(3, new Vector4(t.x, t.y, t.z, 1f));
                                property.SetMatrix4x4Value(matrix4x4);
                            }
                        }
                    }
                    else
                    {
                        var oneX = (Vector3)matrix4x4.GetRow(0);
                        var oneY = (Vector3)matrix4x4.GetRow(1);
                        var oneZ = (Vector3)matrix4x4.GetRow(2);

                        using (var changeCheck = new EditorGUI.ChangeCheckScope())
                        {
                            oneX = EditorGUI.Vector3Field(firstLine, "One X", oneX);
                            oneY = EditorGUI.Vector3Field(secondLine, "One Y", oneY);
                            oneZ = EditorGUI.Vector3Field(thirdLine, "One Z", oneZ);
                            if (changeCheck.changed)
                            {
                                // TODO: Record undo
                                matrix4x4.SetRow(0, oneX);
                                matrix4x4.SetRow(1, oneY);
                                matrix4x4.SetRow(2, oneZ);
                                property.SetMatrix4x4Value(matrix4x4);
                            }
                        }
                    }
                }

            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 4 + EditorGUIUtility.standardVerticalSpacing * 3;
        }
    }
}