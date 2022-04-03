using System.Linq;
using Silksprite.MeshBuilder.Models.Paths;

namespace Silksprite.MeshBuilder.Models.Meshes
{
    public class PolygonMeshieFactory : IMeshieFactory
    {
        readonly IPathieFactory _pathie;
        readonly int _materialIndex;

        public PolygonMeshieFactory(IPathieFactory pathie, int materialIndex)
        {
            _pathie = pathie;
            _materialIndex = materialIndex;
        }

        public Meshie Build(LodMaskLayer lod)
        {
            var pathie = _pathie.Build(lod);

            var vertices = pathie.Active.Vertices.ToArray(); 
            if (vertices.Length < 3) return Meshie.Empty();

            var gons = Enumerable.Range(1, vertices.Length - 2).Select(i => new Gon(new []{ 0, i, i + 1 }, _materialIndex));
            return Meshie.Builder(vertices, gons, true).ToMeshie();
        }
    }
}