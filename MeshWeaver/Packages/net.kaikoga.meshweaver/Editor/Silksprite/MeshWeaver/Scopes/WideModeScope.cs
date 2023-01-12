using System;
using UnityEditor;
using UnityEngine;

namespace Silksprite.MeshWeaver.Scopes
{
    public readonly struct WideModeScope : IDisposable
    {
        readonly bool _oldWideMode;
        
        public WideModeScope(bool wideMode)
        {
            _oldWideMode = EditorGUIUtility.wideMode;
            EditorGUIUtility.wideMode = wideMode;
        }

        public void Dispose()
        {
            EditorGUIUtility.wideMode = _oldWideMode;
        }
    }
}