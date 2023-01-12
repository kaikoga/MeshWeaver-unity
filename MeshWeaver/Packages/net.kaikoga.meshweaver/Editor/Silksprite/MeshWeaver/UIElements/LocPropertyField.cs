using Silksprite.MeshWeaver.Utils;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Silksprite.MeshWeaver.UIElements
{
    public class LocPropertyField : VisualElement
    {
        // Very ugly implementation using EditorGUILayout.PropertyField(), because
        // - it is currently the only simple way to dynamically update label content,
        // - only IMGUI supports custom property attributes, and
        // - only IMGUI is supported by prefab override editor
        public LocPropertyField(SerializedProperty property, LocalizedContent locLabel)
        {
            name = "mw-genericProperty";
            Add(new IMGUIContainer(() =>
            {
                EditorGUI.BeginChangeCheck();
                MeshWeaverGUILayout.PropertyField(property, locLabel);
                if (EditorGUI.EndChangeCheck())
                {
                    property.serializedObject.ApplyModifiedProperties();
                    using (var evt = PropertyChangeEvent.GetPooled()) SendEvent(evt);
                }
            }) { name = "mw-imgui", style =
            {
                paddingTop = new StyleLength(1f),
                paddingBottom = new StyleLength(1f),
                paddingLeft = new StyleLength(1f),
                paddingRight = new StyleLength(1f)
            } });
        }
    }

    public class LocPropertyField2 : PropertyField  
    {
        public LocPropertyField2(SerializedProperty property, LocalizedContent locLabel) : base(property, Localization.Lang == "en" ? property.displayName : locLabel.Tr)
        {
            style.flexGrow = new StyleFloat(1f);
        }
    }
}