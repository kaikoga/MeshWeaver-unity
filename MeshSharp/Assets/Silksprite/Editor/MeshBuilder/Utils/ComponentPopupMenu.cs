using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Silksprite.MeshBuilder.Utils
{
    public class ComponentPopupMenu<T>
    where T : Component
    {
        Type[] _types;
        string[] _menuOptions;

        public ComponentPopupMenu(params Type[] types)
        {
            _types = new [] { (Type)null }.Concat(types).ToArray();
            _menuOptions = new [] { "Create..." }.Concat(types.Select(type => type.Name)).ToArray();
        }

        public T ChildPopup(Transform parent)
        {
            var index = EditorGUILayout.Popup(0, _menuOptions);
            if (index <= 0) return null;

            var gameObject = new GameObject(_menuOptions[index]);
            gameObject.transform.SetParent(parent, false);
            return (T)gameObject.AddComponent(_types[index]);
        }

        public void PropertyField(Component self, ref T property)
        {
            var child = ChildPopup(self.transform);
            if (child != null)
            {
                property = child;
            }
        }

        public void PropertyField(Component self, ref List<T> property)
        {
            using (new GUILayout.HorizontalScope())
            {
                var child = ChildPopup(self.transform);
                if (child != null)
                {
                    property.Add(child);
                }

                if (GUILayout.Button("Collect"))
                {
                    property = self.transform.OfType<Transform>()
                        .Select(t => t.GetComponent<T>())
                        .Where(p => p != null)
                        .ToList();
                    EditorUtility.SetDirty(self);
                }
            }
        }
    }
}