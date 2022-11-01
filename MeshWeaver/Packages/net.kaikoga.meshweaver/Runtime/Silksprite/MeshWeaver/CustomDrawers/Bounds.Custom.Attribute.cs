using UnityEngine;

namespace Silksprite.MeshWeaver.CustomDrawers
{
    public class BoundsCustomAttribute : PropertyAttribute
    {
        public readonly bool preferMinMax;

        public BoundsCustomAttribute(bool preferMinMax = false)
        {
            this.preferMinMax = preferMinMax;
        }
    }
}