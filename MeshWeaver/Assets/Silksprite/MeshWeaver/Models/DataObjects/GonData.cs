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

        public static GonData FromGon(Gon gon, Func<int, int> pin)
        {
            return new GonData
            {
                indices = gon.Indices.ToArray(),
                materialIndex = pin(gon.MaterialIndex)
            };
        }

        public Gon ToGon(Func<int, int> unpin)
        {
            return new Gon(indices, unpin(materialIndex));
        }
    }
}