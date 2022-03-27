using System.Linq;

namespace Silksprite.MeshBuilder.Models.Meshes
{
    public class PolygonMeshieFactory : IMeshieFactory
    {
        readonly Pathie _pathie;

        public PolygonMeshieFactory(Pathie pathie)
        {
            _pathie = pathie;
        }

        public Meshie Build()
        {
            var vertices = _pathie.Active.Vertices.ToArray(); 
            if (vertices.Length < 3) return Meshie.Empty();

            var indices = Enumerable.Range(1, vertices.Length - 2).SelectMany(i => new[] { 0, i, i + 1 });
            return Meshie.Builder(vertices, indices, true).ToMeshie();
        }
    }
}