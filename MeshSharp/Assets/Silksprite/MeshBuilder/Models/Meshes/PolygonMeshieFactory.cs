using System.Linq;

namespace Silksprite.MeshBuilder.Models.Meshes
{
    public class PolygonMeshieFactory : IMeshieFactory<Pathie>
    {
        public Meshie Build(Pathie pathie)
        {
            var meshie = new Meshie();
            var vertices = pathie.ActiveVertices.ToArray(); 
            if (vertices.Length < 3) return meshie;

            meshie.Vertices.AddRange(vertices);
            meshie.Indices.AddRange(Enumerable.Range(1, vertices.Length - 2).SelectMany(i => new[] { 0, i, i + 1 }));
            return meshie;
        }
    }
}