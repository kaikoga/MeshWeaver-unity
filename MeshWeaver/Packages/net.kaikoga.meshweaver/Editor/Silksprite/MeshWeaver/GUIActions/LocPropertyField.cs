using Silksprite.MeshWeaver.GUIActions.Events;
using Silksprite.MeshWeaver.Utils;
using UnityEditor;

namespace Silksprite.MeshWeaver.GUIActions
{
    public class LocPropertyField : GUIAction
    {
        readonly SerializedProperty _property;
        readonly LocalizedContent _locLabel;

        public readonly Dispatcher<PropertyChangeEvent> onPropertyChanged = new Dispatcher<PropertyChangeEvent>();

        public LocPropertyField(SerializedProperty property, LocalizedContent locLabel)
        {
            _property = property;
            _locLabel = locLabel;
        }

        public override void OnGUI()
        {
            EditorGUI.BeginChangeCheck();
            MeshWeaverGUILayout.PropertyField(_property, _locLabel);
            if (EditorGUI.EndChangeCheck())
            {
                _property.serializedObject.ApplyModifiedProperties();
                onPropertyChanged.Invoke();
            }
        }
    }
}