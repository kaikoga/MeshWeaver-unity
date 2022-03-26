using System.Linq;

namespace Silksprite.MeshBuilder.Models.Meshes
{
    public class PolygonMeshieFactory : IMeshieFactory<Pathie>
    {
        public Meshie Build(Pathie pathie)
        {
            var vertices = pathie.Active.Vertices.ToArray(); 
            if (vertices.Length < 3) return new Meshie();

            var indices = Enumerable.Range(1, vertices.Length - 2).SelectMany(i => new[] { 0, i, i + 1 });
            return new Meshie(vertices, indices);
        }
    }
}