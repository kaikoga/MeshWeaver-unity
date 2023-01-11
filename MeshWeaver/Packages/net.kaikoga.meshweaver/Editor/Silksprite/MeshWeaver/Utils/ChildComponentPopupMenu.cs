using System;
using System.Linq;
using Silksprite.MeshWeaver.Controllers.Extensions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Utils
{
    public class ChildComponentPopupMenu<T>
    where T : Component
    {
        readonly Type[] _types;
        readonly LocalizedContent[] _menuOptions;

        public ChildComponentPopupMenu(params Type[] types)
        {
            _types = new [] { (Type)null, null }.Concat(types).ToArray();
            _menuOptions = new [] { Loc("Create Child..."), _Loc("") }.Concat(types.Select(type => type == typeof(void) ? _Loc("") : _Loc(type.Name))).ToArray();
        }

        public VisualElement VisualElement(Component self, string label) => VisualElement(self, label, null, null);
        public VisualElement VisualElement(Component self, string name, SerializedProperty serializedProperty) => VisualElement(self, " ", name, serializedProperty);

        VisualElement VisualElement(Component self, string label, string name, SerializedProperty serializedProperty)
        {
            return new IMGUIContainer(() =>
            {
                var index = EditorGUILayout.Popup(label, 0, _menuOptions.Select(loc => loc.Tr).ToArray());
                if (index <= 0) return;

                var child = self.AddChildComponent<T>(_types[index]);
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
        }
    }
}