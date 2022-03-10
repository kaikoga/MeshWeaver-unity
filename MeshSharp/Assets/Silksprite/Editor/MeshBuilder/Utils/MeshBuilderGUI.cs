using System;
using UnityEditor;

namespace Silksprite.MeshBuilder.Utils
{
    public static class MeshBuilderGUI
    {
        public static void DumpFoldout(string title, ref bool isExpanded, Func<object> dump)
        {
            isExpanded = EditorGUILayout.Foldout(isExpanded, title);
            if (isExpanded)
            {
                EditorGUILayout.TextArea(dump()?.ToString() ?? "null");
            }
        }
    }
}