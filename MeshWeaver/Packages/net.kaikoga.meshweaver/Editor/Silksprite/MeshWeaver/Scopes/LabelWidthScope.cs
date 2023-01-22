using System;
using UnityEditor;

namespace Silksprite.MeshWeaver.Scopes
{
    public class LabelWidthScope : IDisposable
    {
        readonly float _oldLabelWidth;

        public LabelWidthScope(float labelWidth)
        {
            _oldLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = labelWidth;
        }

        public void Dispose()
        {
            EditorGUIUtility.labelWidth = _oldLabelWidth;
        }
    }
}