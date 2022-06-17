using UnityEngine;

namespace Silksprite.MeshWeaver.CustomDrawers
{
    public class RectCustomAttribute : PropertyAttribute
    {
        public readonly bool preferMinMax;

        public RectCustomAttribute(bool preferMinMax = false)
        {
            this.preferMinMax = preferMinMax;
        }
    }
}