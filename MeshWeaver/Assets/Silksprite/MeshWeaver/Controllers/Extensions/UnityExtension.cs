using System;
using System.Collections.Generic;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Extensions
{
    public static class UnityEditorExtension
    {
        public static T AddChildComponent<T>(this Component self, string name = null) where T : Component
        {
            var gameObject = new GameObject(name ?? typeof(T).Name);
            gameObject.transform.SetParent(self.transform, false);
            return gameObject.AddComponent<T>();
        }

        public static T AddChildComponent<T>(this Component self, Type type, string name = null) where T : Component
        {
            var gameObject = new GameObject(name ?? type.Name);
            gameObject.transform.SetParent(self.transform, false);
            return (T)gameObject.AddComponent(type);
        }

        public static IEnumerable<T> GetComponentsInDirectChildren<T>(this Component self) where T : Component
        {
            foreach (Transform child in self.transform)
            {
                if (child.TryGetComponent<T>(out var c)) yield return c;
            }
        }
    }
}