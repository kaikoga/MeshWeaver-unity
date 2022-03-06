using System;
using System.Linq;

namespace Silksprite.MeshBuilder.Models.DataObjects
{
    [Serializable]
    public class MeshieData
    {
        public VertieData[] vertices;
        public int[] indices;

        public static MeshieData FromMeshie(Meshie meshie)
        {
            return new MeshieData
            {
                vertices = meshie.Vertices.Select(VertieData.FromVertie).ToArray(),
                indices = meshie.Indices.ToArray()
            };
        }

        public Meshie ToMeshie()
        {
            return new Meshie(vertices.Select(v => v.ToVertie()), indices);
        }
    }
}