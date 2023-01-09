using Silksprite.MeshWeaver.Utils;
using UnityEditor;
using UnityEngine.UIElements;

namespace Silksprite.MeshWeaver.UIElements
{
    public class LocPropertyField : VisualElement  
    {
        // Very ugly implementation using EditorGUILayout.PropertyField(), because it is currently the only simple way to dynamically update label content
        public LocPropertyField(SerializedProperty property, LocalizedContent locLabel)
        {
            name = "mw-genericProperty";
            Add(new IMGUIContainer(() =>
            {
                EditorGUI.BeginChangeCheck();
                MeshWeaverGUILayout.PropertyField(property, locLabel);
                if (EditorGUI.EndChangeCheck()) property.serializedObject.ApplyModifiedProperties();
            }) { name = "mw-imgui" });
        }
    }
}