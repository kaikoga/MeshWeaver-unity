using System;
using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshBuilder.Extensions;
using UnityEditor;
using UnityEngine;

namespace Silksprite.MeshBuilder.Utils
{
    public class ChildComponentPopupMenu<T>
    where T : Component
    {
        readonly Type[] _types;
        readonly string[] _menuOptions;

        public ChildComponentPopupMenu(params Type[] types)
        {
            _types = new [] { (Type)null }.Concat(types).ToArray();
            _menuOptions = new [] { "Create Child..." }.Concat(types.Select(type => type.Name)).ToArray();
        }

        T ChildPopup(Component self)
        {
            var index = EditorGUILayout.Popup(0, _menuOptions);
            return index <= 0 ? null : self.AddChildComponent<T>(_types[index]);
        }

        public void PropertyField(Component self, ref T property)
        {
            var child = ChildPopup(self);
            if (child != null)
            {
                property = child;
            }
        }

        public void PropertyField(Component self, ref List<T> property)
        {
            using (new GUILayout.HorizontalScope())
            {
                var child = ChildPopup(self);
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