using System;
using Silksprite.MeshWeaver.Models;
using Silksprite.MeshWeaver.Utils;
using UnityEditor;
using UnityEngine.UIElements;

namespace Silksprite.MeshWeaver.UIElements
{
    public class DumpFoldout : VisualElement
    {
        public DumpFoldout(LocalizedContent titlePrefix, Func<IDump> dump)
        {
            var isExpanded = false;
            var dumped = dump();
            // FIXME
            Add(new IMGUIContainer(() =>
            {
                using (var changed = new EditorGUI.ChangeCheckScope())
                {
                    isExpanded = EditorGUILayout.Foldout(isExpanded, $"{titlePrefix.Tr}: {dumped}");
                    if (changed.changed) dumped = dump();
                }
                if (isExpanded)
                {
                    EditorGUILayout.TextArea(dumped?.Dump() ?? "null");
                }
            }));
        }
    }
}