using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
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

        public static IEnumerable<T> GetComponentsInDirectChildren<T>(this Component self) where T : Component
        {
            foreach (Transform child in self.transform)
            {
                if (child.TryGetComponent<T>(out var c)) yield return c;
            }
        }

        public static void CollectDirectChildren<T>(this Component self, out List<T> property) where T : Component
        {
            property = self.GetComponentsInDirectChildren<T>().ToList();
            EditorUtility.SetDirty(self);
        }
    }
}