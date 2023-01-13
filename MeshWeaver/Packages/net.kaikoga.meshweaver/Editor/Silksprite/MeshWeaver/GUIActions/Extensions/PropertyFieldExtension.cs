using Silksprite.MeshWeaver.GUIActions.Events;
using UnityEngine;
using UnityEngine.UIElements;

namespace Silksprite.MeshWeaver.GUIActions.Extensions
{
    public static class PropertyFieldExtension
    {
        public static void RegisterValueChangedCallback(this LocToggle toggle, EventCallback<ChangeEvent<bool>> callback)
        {
            toggle.onValueChanged.Add(callback);
        }

        public static void UnregisterValueChangedCallback(this LocToggle toggle, EventCallback<ChangeEvent<bool>> callback)
        {
            Debug.LogError("not implemented");
        }

        public static void RegisterPropertyChangedCallback<T>(this LocPropertyField propertyField, EventCallback<PropertyChangeEvent> callback)
        {
            propertyField.onPropertyChanged.Add(callback);
        }

        public static void UnregisterPropertyChangedCallback<T>(this LocPropertyField propertyField, EventCallback<PropertyChangeEvent> callback)
        {
            Debug.LogError("not implemented");
        }

    }
}
