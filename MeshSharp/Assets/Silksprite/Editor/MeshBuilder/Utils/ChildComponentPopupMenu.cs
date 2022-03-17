using System;
using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshBuilder.Controllers.Base;
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
            _menuOptions = new [] { "Create Child...", "" }.Concat(types.Select(type => type == typeof(void) ? "" : type.Name)).ToArray();
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
                    if (self is GeometryProvider g) g.RefreshElements();
                }

                if (GUILayout.Button("Collect"))
                {
                    self.CollectDirectChildren(out property);
                }
            }
        }
    }
}