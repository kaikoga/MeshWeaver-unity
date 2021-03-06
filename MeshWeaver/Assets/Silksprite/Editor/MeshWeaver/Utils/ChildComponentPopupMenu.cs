using System;
using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Extensions;
using UnityEditor;
using UnityEngine;

namespace Silksprite.MeshWeaver.Utils
{
    public class ChildComponentPopupMenu<T>
    where T : Component
    {
        readonly Type[] _types;
        readonly string[] _menuOptions;

        public ChildComponentPopupMenu(params Type[] types)
        {
            _types = new [] { (Type)null, null }.Concat(types).ToArray();
            _menuOptions = new [] { "Create Child...", "" }.Concat(types.Select(type => type == typeof(void) ? "" : type.Name)).ToArray();
        }

        public T ChildPopup(Component self)
        {
            var index = EditorGUILayout.Popup(0, _menuOptions);
            return index <= 0 ? null : self.AddChildComponent<T>(_types[index]);
        }

        public void PropertyField(Component self, string name, ref T property)
        {
            var child = ChildPopup(self);
            if (child != null)
            {
                property = child;
                property.name = $"{property.name}_{name}";
            }
        }
    }
}