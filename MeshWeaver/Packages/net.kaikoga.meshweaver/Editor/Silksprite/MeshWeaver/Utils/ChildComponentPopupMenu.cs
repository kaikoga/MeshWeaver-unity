using System;
using System.Linq;
using Silksprite.MeshWeaver.Controllers.Extensions;
using Silksprite.MeshWeaver.GUIActions;
using UnityEditor;
using UnityEngine;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Utils
{
    public class ChildComponentPopupMenu<T>
    where T : Component
    {
        readonly Type[] _types;

        public ChildComponentPopupMenu(params Type[] types)
        {
            _types = types;
        }

        public GUIAction ToGUIAction(Component self, LocalizedContent? label) => ToGUIAction(self, label, null, null);
        public GUIAction ToGUIAction(Component self, string name, SerializedProperty serializedProperty) => ToGUIAction(self, null, name, serializedProperty);

        GUIAction ToGUIAction(Component self, LocalizedContent? label, string name, SerializedProperty serializedProperty)
        {
            return new LocPopupButtons(label ?? _Loc(" "), Loc("Create Child..."), _types.Select(type =>
            {
                if (type == typeof(void) || type == null) return new LocMenuItem(_Loc(""), null);
                return new LocMenuItem(_Loc(type.Name), () =>
                {
                    var child = self.AddChildComponent<T>(type);
                    if (serializedProperty == null) return;

                    child.name = $"{child.name}_{name}";
                    if (serializedProperty.isArray)
                    {
                        var size = serializedProperty.arraySize;
                        serializedProperty.InsertArrayElementAtIndex(size);
                        serializedProperty.GetArrayElementAtIndex(size).objectReferenceValue = child;
                    }
                    else
                    {
                        serializedProperty.objectReferenceValue = child;
                    }
                    serializedProperty.serializedObject.ApplyModifiedProperties();
                });
            }).ToArray());
        }
    }
}