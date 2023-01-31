using System;
using System.Linq;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models.DataObjects
{
    [Serializable]
    public class GonData
    {
        public int[] indices;
        public int materialIndex;

        public static GonData FromGon(Gon gon, Func<Material, int> pin)
        {
            return new GonData
            {
                indices = gon.Indices.ToArray(),
                materialIndex = pin(gon.material)
            };
        }

        public Gon ToGon(Func<int, Material> unpin)
        {
            return new Gon(indices, unpin(materialIndex));
        }
    }
}