using System.Linq;

namespace Silksprite.MeshBuilder.Models.Meshes
{
    public class PolygonMeshieFactory
    {
        public Meshie Build(Pathie pathie, Meshie meshie)
        {
            if (pathie.Vertices.Count < 3) return meshie;

            meshie.Vertices.AddRange(pathie.Vertices);
            meshie.Indices.AddRange(Enumerable.Range(1, pathie.Vertices.Count - 2).SelectMany(i => new[] { 0, i, i + 1 }));
            return meshie;
        }
    }
}