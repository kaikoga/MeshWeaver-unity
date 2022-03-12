using System;

namespace Silksprite.MeshBuilder.Models
{
    [Flags]
    public enum LodMask
    {
        LOD0 = 1,
        LOD1 = 2,
        LOD2 = 4,
        Collider = 65536,
        All = -1,
        AllRenderer = 7
    }

    public enum LodMaskLayer
    {
        LOD0 = 1,
        LOD1 = 2,
        LOD2 = 4,
        Collider = 65536
    }
}