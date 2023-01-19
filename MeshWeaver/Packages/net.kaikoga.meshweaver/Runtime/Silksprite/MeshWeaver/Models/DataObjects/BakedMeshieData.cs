using System;

namespace Silksprite.MeshWeaver.Models.DataObjects
{
    [Serializable]
    public class BakedMeshieData
    {
        public LodMaskLayer[] lodMaskLayers;
        public MeshieData meshData;
    }
}