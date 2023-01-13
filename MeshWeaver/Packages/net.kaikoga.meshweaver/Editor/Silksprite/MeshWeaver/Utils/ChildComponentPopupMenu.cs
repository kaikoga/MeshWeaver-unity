using System;
using System.Linq;
using Silksprite.MeshWeaver.Controllers.Extensions;
using Silksprite.MeshWeaver.GUIActions;
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

        public VisualElement VisualElement(Component self, LocalizedContent? label) => VisualElement(self, label, null, null);
        public VisualElement VisualElement(Component self, string name, SerializedProperty serializedProperty) => VisualElement(self, null, name, serializedProperty);

        public GUIAction ToGUIAction(Component self, LocalizedContent? label) => ToGUIAction(self, label, null, null);
        public GUIAction ToGUIAction(Component self, string name, SerializedProperty serializedProperty) => ToGUIAction(self, null, name, serializedProperty);

        VisualElement VisualElement(Component self, LocalizedContent? label, string name, SerializedProperty serializedProperty)
        {
            var guiAction = ToGUIAction(self, label, name, serializedProperty);
            return new IMGUIContainer(() => guiAction.OnGUI());
        }

        GUIAction ToGUIAction(Component self, LocalizedContent? label, string name, SerializedProperty serializedProperty)
        {
            return GUIAction.Build(() =>
            {
                var index = EditorGUILayout.Popup(label?.Tr ?? " ", 0, _menuOptions.Select(loc => loc.Tr).ToArray());
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