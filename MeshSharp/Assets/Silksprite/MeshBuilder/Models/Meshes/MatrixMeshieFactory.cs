using System.Linq;
using Silksprite.MeshBuilder.Extensions;

namespace Silksprite.MeshBuilder.Models.Meshes
{
    public class MatrixMeshieFactory : IMeshieFactory
    {
        readonly Pathie _pathieX;
        readonly Pathie _pathieY;

        public MatrixMeshieFactory(Pathie pathieX, Pathie pathieY)
        {
            _pathieX = pathieX;
            _pathieY = pathieY;
        }

        public Meshie Build()
        {
            var activeX = _pathieX.Active;
            var countX = activeX.Vertices.Length;
            if (countX < 2) return Meshie.Empty();
            var activeY = _pathieY.Active;
            var countY = activeY.Vertices.Length;
            if (countY < 2) return Meshie.Empty();

            var indicesX = _pathieX.ChangingIndices((a, b) => a.TranslationEquals(b, 0f));
            var indicesY = _pathieY.ChangingIndices((a, b) => a.TranslationEquals(b, 0f));

            var vertices = activeY.Vertices.SelectMany(pY => activeX.Vertices.Select(pX => pX * pY));
            var indices = indicesY
                .SelectMany(iY => indicesX.Select(i => i + iY * countX))
                .SelectMany(i => new[] { i, i + countX, i + countX + 1, i, i + countX + 1, i + 1 });
            return Meshie.Builder(vertices, indices, true).ToMeshie();
        }
    }
}