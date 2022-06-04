using System;
using System.Linq;

namespace Silksprite.MeshWeaver.Models.DataObjects
{
    [Serializable]
    public class MeshieData
    {
        public VertieData[] vertices;
        public GonData[] gons;

        public static MeshieData FromMeshie(Meshie meshie, Func<int, int> pin)
        {
            return new MeshieData
            {
                vertices = meshie.Vertices.Select(VertieData.FromVertie).ToArray(),
                gons = meshie.Gons.Select(gon => GonData.FromGon(gon, pin)).ToArray()
            };
        }

        public Meshie ToMeshie(Func<int, int> unpin)
        {
            return new Meshie(vertices.Select(v => v.ToVertie()), gons.Select(g => g.ToGon(unpin)));
        }
    }
}