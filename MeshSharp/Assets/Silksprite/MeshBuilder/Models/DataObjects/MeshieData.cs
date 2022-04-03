using System;
using System.Linq;

namespace Silksprite.MeshBuilder.Models.DataObjects
{
    [Serializable]
    public class MeshieData
    {
        public VertieData[] vertices;
        public GonData[] gons;

        public static MeshieData FromMeshie(Meshie meshie)
        {
            return new MeshieData
            {
                vertices = meshie.Vertices.Select(VertieData.FromVertie).ToArray(),
                gons = meshie.Gons.Select(GonData.FromGon).ToArray()
            };
        }

        public Meshie ToMeshie()
        {
            return new Meshie(vertices.Select(v => v.ToVertie()), gons.Select(g => g.ToGon()));
        }
    }
}