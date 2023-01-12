using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Silksprite.MeshWeaver.UIElements.Extensions
{
    public static class PropertyFieldExtension
    {
        public static bool RegisterPropertyChangedCallback<T>(this PropertyField propertyField, EventCallback<ChangeEvent<T>> callback)
        {
            if (!(propertyField is CallbackEventHandler callbackEventHandler)) return false;
            callbackEventHandler.RegisterCallback(callback);
            return true;
        }

        public static bool UnregisterPropertyChangedCallback<T>(this PropertyField propertyField, EventCallback<ChangeEvent<T>> callback)
        {
            if (!(propertyField is CallbackEventHandler callbackEventHandler)) return false;
            callbackEventHandler.UnregisterCallback(callback);
            return true;
        }
        
        public static bool RegisterPropertyChangedCallback<T>(this LocPropertyField propertyField, EventCallback<PropertyChangeEvent> callback)
        {
            if (!(propertyField is CallbackEventHandler callbackEventHandler)) return false;
            callbackEventHandler.RegisterCallback(callback);
            return true;
        }

        public static bool UnregisterPropertyChangedCallback<T>(this LocPropertyField propertyField, EventCallback<PropertyChangeEvent> callback)
        {
            if (!(propertyField is CallbackEventHandler callbackEventHandler)) return false;
            callbackEventHandler.UnregisterCallback(callback);
            return true;
        }

    }
}
