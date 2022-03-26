using System.Linq;
using Silksprite.MeshBuilder.Models.DataObjects;

namespace Silksprite.MeshBuilder.Models.Paths
{
    public class BakedPathieFactory : IPathieFactory
    {
        readonly PathieData _data;

        public BakedPathieFactory(PathieData data)
        {
            _data = data;
        }

        public Pathie Build()
        {
            if (_data == null) return new Pathie();

            var vertices = _data.vertices.Select(v => v.ToVertie());
            return new Pathie(vertices);
        }
    }
}