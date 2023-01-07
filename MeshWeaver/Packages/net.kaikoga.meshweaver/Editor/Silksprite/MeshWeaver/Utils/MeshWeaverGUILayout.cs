using Silksprite.MeshWeaver.Controllers;
using UnityEditor;
using UnityEngine;

namespace Silksprite.MeshWeaver.Utils
{
    public static class MeshWeaverGUILayout
    {
        public static void Header(LocalizedContent text)
        {
            GUILayout.Label(text.Tr, MeshWeaverSkin.Header);
        }

        public static void PropertyField(SerializedProperty serializedProperty, LocalizedContent label)
        {
            if (MeshWeaverSettings.Current.lang == "en")
            {
                EditorGUILayout.PropertyField(serializedProperty);
            }
            else
            {
                EditorGUILayout.PropertyField(serializedProperty, label.GUIContent);
            }
        }
    }
}