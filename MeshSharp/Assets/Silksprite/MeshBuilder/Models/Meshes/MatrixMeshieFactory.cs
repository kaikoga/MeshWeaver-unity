using System.Linq;
using Silksprite.MeshBuilder.Extensions;
using Silksprite.MeshBuilder.Models.Paths;

namespace Silksprite.MeshBuilder.Models.Meshes
{
    public class MatrixMeshieFactory : IMeshieFactory
    {
        readonly IPathieFactory _pathieX;
        readonly IPathieFactory _pathieY;

        public MatrixMeshieFactory(IPathieFactory pathieX, IPathieFactory pathieY)
        {
            _pathieX = pathieX;
            _pathieY = pathieY;
        }

        public Meshie Build(LodMaskLayer lod)
        {
            var pathieX = _pathieX.Build(lod);
            var pathieY = _pathieY.Build(lod);

            var activeX = pathieX.Active;
            var countX = activeX.Vertices.Length;
            if (countX < 2) return Meshie.Empty();
            var activeY = pathieY.Active;
            var countY = activeY.Vertices.Length;
            if (countY < 2) return Meshie.Empty();

            var indicesX = pathieX.ChangingIndices((a, b) => a.TranslationEquals(b, 0f));
            var indicesY = pathieY.ChangingIndices((a, b) => a.TranslationEquals(b, 0f));

            var vertices = activeY.Vertices.SelectMany(pY => activeX.Vertices.Select(pX => pX * pY));
            var indices = indicesY
                .SelectMany(iY => indicesX.Select(i => i + iY * countX))
                .SelectMany(i => new[] { i, i + countX, i + countX + 1, i, i + countX + 1, i + 1 });
            return Meshie.Builder(vertices, indices, true).ToMeshie();
        }
    }
}