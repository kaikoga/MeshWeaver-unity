using System;
using Silksprite.MeshWeaver.Utils;

namespace Silksprite.MeshWeaver.GUIActions
{
    public class LocMenuItem
    {
        public readonly LocalizedContent Label;
        public readonly Action OnSelect;

        public LocMenuItem(LocalizedContent label, Action onSelect)
        {
            Label = label;
            OnSelect = onSelect;
        }
    }
}