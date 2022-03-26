using System.Linq;
using Silksprite.MeshBuilder.Extensions;

namespace Silksprite.MeshBuilder.Models.Meshes
{
    public class MatrixMeshieFactory : IMeshieFactory<Pathie, Pathie>
    {
        public Meshie Build(Pathie pathieX, Pathie pathieY)
        {
            // FIXME: not "Iterate active pathies", but "Cull invalid triangles"  
            var meshie = new Meshie();
            var activeX = pathieX.Active;
            var countX = activeX.Vertices.Count;
            if (countX < 2) return meshie;
            var activeY = pathieY.Active;
            var countY = activeY.Vertices.Count;
            if (countY < 2) return meshie;

            meshie.Vertices.AddRange(activeY.Vertices.SelectMany(pY => activeX.Vertices.Select(pX => pX * pY)));
            var indicesX = pathieX.ChangingIndices((a, b) => a.VertexEquals(b, 0f));
            var indicesY = pathieY.ChangingIndices((a, b) => a.VertexEquals(b, 0f));
            meshie.Indices.AddRange(indicesY
                .SelectMany(iY => indicesX.Select(i => i + iY * countX))
                .SelectMany(i => new[] { i, i + countX, i + countX + 1, i, i + countX + 1, i + 1 }));
            return meshie;
        }
    }
}