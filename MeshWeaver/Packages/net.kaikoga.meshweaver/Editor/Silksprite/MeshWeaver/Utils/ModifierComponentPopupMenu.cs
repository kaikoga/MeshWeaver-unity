using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Utils
{
    public class ModifierComponentPopupMenu<T>
    {
        readonly Type[] _types;
        readonly LocalizedContent[] _menuOptions;

        public ModifierComponentPopupMenu(params Type[] types)
        {
            _types = new [] { (Type)null, null }.Concat(types).ToArray();
            _menuOptions = new [] { Loc("Add Modifier..."), _Loc("") }.Concat(types.Select(type => type == typeof(void) ? _Loc("") : _Loc(type.Name))).ToArray();
        }

        public T ModifierPopup(Component self, LocalizedContent? label = null)
        {
            var index = EditorGUILayout.Popup((label ?? Loc("Modifiers")).Tr, 0, _menuOptions.Select(x => x.Tr).ToArray());
            if (index <= 0) return default;

            return (T)(object)self.gameObject.AddComponent(_types[index]);
        }
        
        public VisualElement VisualElement(Component self, LocalizedContent? label = null)
        {
            return new IMGUIContainer(() => ModifierPopup(self, label));
        }
    }
}