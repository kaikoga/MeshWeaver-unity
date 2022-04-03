using System;
using System.Linq;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models.DataObjects
{
    [Serializable]
    public class GonData
    {
        public int[] indices;
        public int materialIndex;

        public static GonData FromGon(Gon gon)
        {
            return new GonData
            {
                indices = gon.Indices.ToArray(),
                materialIndex = gon.MaterialIndex
            };
        }

        public Gon ToGon()
        {
            return new Gon(indices, materialIndex);
        }
    }
}