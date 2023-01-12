using Silksprite.MeshWeaver.Utils;
using UnityEngine.UIElements;

namespace Silksprite.MeshWeaver.UIElements
{
    public class LocToggle : Toggle
    {
        LocalizedContent _loc;

        public LocalizedContent loc
        {
            get => _loc;
            set
            {
                _loc = value;
                label = value.Tr;
            }
        }

        public LocToggle(LocalizedContent loc)
        {
            name = loc.Id;
            this.loc = loc;
        }
    }
}