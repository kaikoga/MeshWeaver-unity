using System;
using Silksprite.MeshWeaver.Utils;
using UnityEngine.UIElements;

namespace Silksprite.MeshWeaver.UIElements
{
    public class LocButton : Button
    {
        LocalizedContent _loc;

        public LocalizedContent loc
        {
            get => _loc;
            set
            {
                _loc = value;
                text = value.Tr;
            }
        }

        public LocButton(LocalizedContent loc, Action clickEvent) : base(clickEvent)
        {
            name = loc.Id;
            this.loc = loc;
        }
    }
}