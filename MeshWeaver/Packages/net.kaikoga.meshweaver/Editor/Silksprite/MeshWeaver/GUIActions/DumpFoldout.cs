using System;
using Silksprite.MeshWeaver.Models;
using Silksprite.MeshWeaver.Utils;
using UnityEditor;

namespace Silksprite.MeshWeaver.GUIActions
{
    public class DumpFoldout : GUIContainer
    {
        readonly LocalizedContent _titlePrefix;
        readonly Func<IDump> _dump;

        bool _isExpanded;
        IDump _dumped;

        public DumpFoldout(LocalizedContent titlePrefix, Func<IDump> dump)
        {
            _titlePrefix = titlePrefix;
            _dump = dump;
            _dumped = dump();
        }

        public override void OnGUI()
        {
            using (var changed = new EditorGUI.ChangeCheckScope())
            {
                _isExpanded = EditorGUILayout.Foldout(_isExpanded, $"{_titlePrefix.Tr}: {_dumped}");
                if (changed.changed) _dumped = _dump();
            }
            if (_isExpanded)
            {
                EditorGUILayout.TextArea(_dumped?.Dump() ?? "null");
            }
        }
    }
}