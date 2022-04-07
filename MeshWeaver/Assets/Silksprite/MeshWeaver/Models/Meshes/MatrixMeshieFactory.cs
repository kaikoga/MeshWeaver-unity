using System;
using System.Linq;
using Silksprite.MeshWeaver.Models.Extensions;
using Silksprite.MeshWeaver.Models.Paths;

namespace Silksprite.MeshWeaver.Models.Meshes
{
    public class MatrixMeshieFactory : IMeshieFactory
    {
        readonly IPathieFactory _pathieX;
        readonly IPathieFactory _pathieY;

        readonly OperatorKind _operatorKind;
        readonly int _materialIndex;

        public MatrixMeshieFactory(IPathieFactory pathieX, IPathieFactory pathieY, OperatorKind operatorKind, int materialIndex)
        {
            _pathieX = pathieX;
            _pathieY = pathieY;
            _operatorKind = operatorKind;
            _materialIndex = materialIndex;
        }

        public Meshie Build(LodMaskLayer lod)
        {
            var pathieX = _pathieX.Build(lod);
            var pathieY = _pathieY.Build(lod);

            var activeX = pathieX.Active;
            var countX = activeX.Vertices.Count;
            if (countX < 2) return Meshie.Empty();
            var activeY = pathieY.Active;
            var countY = activeY.Vertices.Count;
            if (countY < 2) return Meshie.Empty();

            var indicesX = pathieX.ChangingIndices((a, b) => a.TranslationEquals(b, 0f));
            var indicesY = pathieY.ChangingIndices((a, b) => a.TranslationEquals(b, 0f));

            var vertices = activeY.Vertices.SelectMany(pY => activeX.Vertices.Select(pX =>
            {
                switch (_operatorKind)
                {
                    case OperatorKind.TranslateOnly:
                        return pX + pY.Vertex;
                    case OperatorKind.ApplyX:
                        return pX * pY;
                    case OperatorKind.ApplyY:
                        return pY * pX;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }));
            var indices = indicesY.SelectMany(y =>
                {
                    return indicesX.Select(x =>
                    {
                        var i = x + y * countX;
                        var a = new[] { i, i + 1, i + countX, i + countX + 1 };
                        return new { x, y, a };
                    });
                }).SelectMany(xya =>
                {
                    var a = xya.a;
                    return new[] { a[0], a[2], a[3], a[0], a[3], a[1] };
                });
            return Meshie.Builder(vertices, indices, _materialIndex, true).ToMeshie();
        }

        public enum OperatorKind
        {
            TranslateOnly = 0,
            ApplyX = 1,
            ApplyY = 2
        }
    }
}