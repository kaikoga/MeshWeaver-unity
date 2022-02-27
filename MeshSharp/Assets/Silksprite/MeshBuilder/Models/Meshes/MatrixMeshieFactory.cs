using System.Linq;

namespace Silksprite.MeshBuilder.Models.Meshes
{
    public class MatrixMeshieFactory
    {
        public Meshie Build(Pathie pathieX, Pathie pathieY, Meshie meshie)
        {
            var countX = pathieX.Vertices.Count;
            if (countX < 2) return meshie;
            var countY = pathieY.Vertices.Count;
            if (countY < 2) return meshie;

            meshie.Vertices.AddRange(pathieY.Vertices.SelectMany(pY => pathieX.Select(pX => pX + pY)));
            meshie.Indices.AddRange(Enumerable.Range(0, countY - 1)
                .SelectMany(iY => Enumerable.Range(iY * countX, countX - 1))
                .SelectMany(i => new[] { i, i + countX, i + countX + 1, i, i + countX + 1, i + 1 }));
            return meshie;
        }
    }
}