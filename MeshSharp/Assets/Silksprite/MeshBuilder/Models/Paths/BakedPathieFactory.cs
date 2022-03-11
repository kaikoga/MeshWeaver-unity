using System.Linq;
using Silksprite.MeshBuilder.Models.DataObjects;

namespace Silksprite.MeshBuilder.Models.Paths
{
    public class BakedPathieFactory
    {
        readonly PathieData _data;

        public BakedPathieFactory(PathieData data)
        {
            _data = data;
        }

        public Pathie Build()
        {
            var pathie = new Pathie();
            if (_data == null) return pathie;
            pathie.Vertices.AddRange(_data.vertices.Select(v => v.ToVertie()));
            return pathie;
        }
    }
}