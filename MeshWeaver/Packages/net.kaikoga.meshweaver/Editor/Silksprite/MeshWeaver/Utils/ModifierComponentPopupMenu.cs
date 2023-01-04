using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using static Silksprite.MeshWeaver.Utils.Localization;

namespace Silksprite.MeshWeaver.Utils
{
    public class ModifierComponentPopupMenu<T>
    {
        readonly Type[] _types;
        readonly string[] _menuOptions;

        public ModifierComponentPopupMenu(params Type[] types)
        {
            _types = new [] { (Type)null, null }.Concat(types).ToArray();
            _menuOptions = new [] { Tr("Add Modifier..."), "" }.Concat(types.Select(type => type == typeof(void) ? "" : type.Name)).ToArray();
        }

        public T ModifierPopup(Component self, string label = null)
        {
            var index = EditorGUILayout.Popup(label ?? Tr("Modifiers"), 0, _menuOptions);
            if (index <= 0) return default;

            return (T)(object)self.gameObject.AddComponent(_types[index]);
        }
    }
}