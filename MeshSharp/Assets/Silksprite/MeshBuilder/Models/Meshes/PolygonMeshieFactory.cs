using System.Linq;
using Silksprite.MeshBuilder.Models.Paths;

namespace Silksprite.MeshBuilder.Models.Meshes
{
    public class PolygonMeshieFactory : IMeshieFactory
    {
        readonly IPathieFactory _pathie;

        public PolygonMeshieFactory(IPathieFactory pathie)
        {
            _pathie = pathie;
        }

        public Meshie Build(LodMaskLayer lod)
        {
            var pathie = _pathie.Build(lod);

            var vertices = pathie.Active.Vertices.ToArray(); 
            if (vertices.Length < 3) return Meshie.Empty();

            var indices = Enumerable.Range(1, vertices.Length - 2).SelectMany(i => new[] { 0, i, i + 1 });
            return Meshie.Builder(vertices, indices, true).ToMeshie();
        }
    }
}