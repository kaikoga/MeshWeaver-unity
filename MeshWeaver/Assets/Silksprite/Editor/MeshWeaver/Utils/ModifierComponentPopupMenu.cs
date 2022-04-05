using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Silksprite.MeshWeaver.Utils
{
    public class ModifierComponentPopupMenu<T>
    where T : Component
    {
        readonly Type[] _types;
        readonly string[] _menuOptions;

        public ModifierComponentPopupMenu(params Type[] types)
        {
            _types = new [] { (Type)null, null }.Concat(types).ToArray();
            _menuOptions = new [] { "Add Modifier...", "" }.Concat(types.Select(type => type == typeof(void) ? "" : type.Name)).ToArray();
        }

        public T ModifierPopup(Component self)
        {
            var index = EditorGUILayout.Popup(0, _menuOptions);
            if (index <= 0) return null;

            return (T)self.gameObject.AddComponent(_types[index]);
        }
    }
}