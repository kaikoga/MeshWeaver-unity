using System;
using System.Linq;
using Silksprite.MeshWeaver.GUIActions;
using UnityEditor;
using UnityEngine;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Utils
{
    public class ModifierComponentPopupMenu<T>
    {
        readonly Type[] _types;

        public ModifierComponentPopupMenu(params Type[] types)
        {
            _types = types;
        }

        public GUIAction ToGUIAction(Component self, LocalizedContent? label = null)
        {
            return new LocPopupButtons(label ?? Loc("Modifiers"), Loc("Add Modifier..."), _types.Select(type =>
            {
                if (type == typeof(void) || type == null) return new LocMenuItem(_Loc(""), null);
                return new LocMenuItem(_Loc(type.Name), () =>
                {
                    self.gameObject.AddComponent(type);
                });
            }).ToArray());
        }
    }
}