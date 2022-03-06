using System;
using UnityEngine;

namespace Silksprite.MeshBuilder.Extensions
{
    public static class UnityExtensions
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
    }
}