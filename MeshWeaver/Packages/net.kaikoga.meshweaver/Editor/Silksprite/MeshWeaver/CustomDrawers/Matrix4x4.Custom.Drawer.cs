using Silksprite.MeshWeaver.Extensions;
using Silksprite.MeshWeaver.Scopes;
using UnityEditor;
using UnityEngine;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.CustomDrawers
{
    [CustomPropertyDrawer(typeof(Matrix4x4CustomAttribute))]
    public class Matrix4x4CustomDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var preferTRS = ((Matrix4x4CustomAttribute)attribute).preferTRS;

            using (new WideModeScope(true))
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
                        isTRS = EditorGUI.ToggleLeft(new Rect(position.xMax - 70f, position.y, 70f, EditorGUIUtility.singleLineHeight), Tr("TRS"), isTRS);
                        if (changeCheck.changed) property.isExpanded = isTRS != preferTRS;
                    }

                    if (GUI.Button(new Rect(position.xMax - 150f, position.y, 70f, EditorGUIUtility.singleLineHeight), Tr("Reset")))
                    {
                        Undo.RecordObject(property.serializedObject.targetObject, $"Reset {property.displayName}"); // XXX: Why am I able to record undo but failing to record action title?
                        matrix4x4 = Matrix4x4.identity;
                        property.SetMatrix4x4Value(matrix4x4);
                    }

                    if (isTRS)
                    {
                        var t = (Vector3)matrix4x4.GetRow(3);
                        matrix4x4.SetRow(3, new Vector4(0f, 0f, 0f, 1f)); // mimic ValidTRS() because rotation and lossyScale requires it
                        var er = matrix4x4.rotation.eulerAngles;
                        var s = matrix4x4.lossyScale;

                        EditorGUI.BeginChangeCheck();
                        t = EditorGUI.Vector3Field(firstLine, Tr("Position"), t);
                        if (EditorGUI.EndChangeCheck()) UpdateTRS($"Inspector Position {property.displayName}");

                        EditorGUI.BeginChangeCheck();
                        er = EditorGUI.Vector3Field(secondLine, Tr("Rotation"), er);
                        if (EditorGUI.EndChangeCheck()) UpdateTRS($"Inspector Rotation {property.displayName}");

                        EditorGUI.BeginChangeCheck();
                        s = EditorGUI.Vector3Field(thirdLine, Tr("Scale"), s);
                        if (EditorGUI.EndChangeCheck()) UpdateTRS($"Inspector Scale {property.displayName}");

                        void UpdateTRS(string name)
                        {
                            Undo.RecordObject(property.serializedObject.targetObject, name); // XXX: Why am I able to record undo but failing to record action title?
                            matrix4x4.SetTRS(Vector3.zero, Quaternion.Euler(er), s);
                            matrix4x4.SetRow(3, new Vector4(t.x, t.y, t.z, 1f));
                            property.SetMatrix4x4Value(matrix4x4);
                        }
                    }
                    else
                    {
                        var oneX = (Vector3)matrix4x4.GetRow(0);
                        var oneY = (Vector3)matrix4x4.GetRow(1);
                        var oneZ = (Vector3)matrix4x4.GetRow(2);

                        EditorGUI.BeginChangeCheck();
                        oneX = EditorGUI.Vector3Field(firstLine, Tr("One X"), oneX);
                        if (EditorGUI.EndChangeCheck()) UpdateOneVectors($"Inspector OneX {property.displayName}");

                        EditorGUI.BeginChangeCheck();
                        oneY = EditorGUI.Vector3Field(secondLine, Tr("One Y"), oneY);
                        if (EditorGUI.EndChangeCheck()) UpdateOneVectors($"Inspector OneY {property.displayName}");

                        EditorGUI.BeginChangeCheck();
                        oneZ = EditorGUI.Vector3Field(thirdLine, Tr("One Z"), oneZ);
                        if (EditorGUI.EndChangeCheck()) UpdateOneVectors($"Inspector OneZ {property.displayName}");

                        void UpdateOneVectors(string name)
                        {
                            Undo.RecordObject(property.serializedObject.targetObject, name); // XXX: Why am I able to record undo but failing to record action title?
                            matrix4x4.SetRow(0, oneX);
                            matrix4x4.SetRow(1, oneY);
                            matrix4x4.SetRow(2, oneZ);
                            property.SetMatrix4x4Value(matrix4x4);
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