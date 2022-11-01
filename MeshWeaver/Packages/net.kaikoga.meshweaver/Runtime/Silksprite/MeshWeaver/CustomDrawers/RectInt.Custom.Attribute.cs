using UnityEngine;

namespace Silksprite.MeshWeaver.CustomDrawers
{
    public class RectIntCustomAttribute : PropertyAttribute
    {
        public readonly bool preferMinMax;

        public RectIntCustomAttribute(bool preferMinMax = false)
        {
            this.preferMinMax = preferMinMax;
        }
    }
}