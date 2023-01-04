using System;
using UnityEngine;

namespace Silksprite.MeshWeaver.Scopes
{
    public readonly struct BackgroundColorScope : IDisposable
    {
        readonly Color _oldColor;
        
        public BackgroundColorScope(Color color)
        {
            _oldColor = GUI.backgroundColor;
            GUI.backgroundColor = color;
        }

        public void Dispose()
        {
            GUI.backgroundColor = _oldColor;
        }
    }
}