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

        public VisualElement VisualElement(Component self, LocalizedContent? label = null)
        {
            return new IMGUIContainer(() =>
            {
                var index = EditorGUILayout.Popup((label ?? Loc("Modifiers")).Tr, 0, _menuOptions.Select(x => x.Tr).ToArray());
                if (index > 0)
                {
                    self.gameObject.AddComponent(_types[index]);
                }
            });
        }
    }
}