using System;
using System.Linq;

namespace Silksprite.MeshWeaver.Models.DataObjects
{
    [Serializable]
    public class PathieData
    {
        public VertieData[] vertices;

        public static PathieData FromPathie(Pathie pathie)
        {
            return new PathieData
            {
                vertices = pathie.Vertices.Select(VertieData.FromVertie).ToArray()
            };
        }

        public Pathie ToPathie()
        {
            return new Pathie(vertices.Select(v => v.ToVertie()));
        }
    }
}