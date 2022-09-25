using UnityEngine;

namespace Silksprite.MeshWeaver.CustomDrawers
{
    public class Matrix4x4CustomAttribute : PropertyAttribute
    {
        public readonly bool preferTRS;

        public Matrix4x4CustomAttribute(bool preferTRS = false)
        {
            this.preferTRS = preferTRS;
        }
    }
}