using System;
using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshWeaver.CustomDrawers;
using Silksprite.MeshWeaver.Models.Paths;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models.Meshes
{
    public class MatrixMeshieFactory : IMeshieFactory
    {
        readonly IPathieFactory _pathieX;
        readonly IPathieFactory _pathieY;

        readonly OperatorKind _operatorKind;
        readonly CellPatternKind _defaultCellPatternKind;
        readonly CellOverride[] _cellPatternOverrides;
        readonly int _materialIndex;

        static readonly Dictionary<CellFormKind, int[][]> CellIndices = new Dictionary<CellFormKind, int[][]>
        {
            [CellFormKind.Default] = new[] { new []{0, 2, 3}, new []{0, 3, 1} },
            [CellFormKind.Right] = new[] { new []{0, 2, 3}, new []{0, 3, 1} }, 
            [CellFormKind.Left] = new[] { new []{0, 2, 1}, new []{1, 2, 3} }, 
            [CellFormKind.TriangleBottomLeft] = new[] { new []{0, 2, 1} }, 
            [CellFormKind.TriangleBottomRight] = new[] { new []{0, 3, 1} }, 
            [CellFormKind.TriangleTopLeft] = new[] { new []{0, 2, 3} }, 
            [CellFormKind.TriangleTopRight] = new[] { new []{1, 2, 3} }, 
            [CellFormKind.None] = Array.Empty<int[]>() 
        };

        public MatrixMeshieFactory(IPathieFactory pathieX, IPathieFactory pathieY,
            OperatorKind operatorKind, CellPatternKind defaultCellPatternKind, CellOverride[] cellPatternOverrides,
            int materialIndex)
        {
            _pathieX = pathieX;
            _pathieY = pathieY;
            _operatorKind = operatorKind;
            _defaultCellPatternKind = defaultCellPatternKind;
            _cellPatternOverrides = cellPatternOverrides;
            _materialIndex = materialIndex;
        }

        public Meshie Build(LodMaskLayer lod)
        {
            var pathieX = _pathieX.Build(lod);
            var pathieY = _pathieY.Build(lod);

            var countX = pathieX.Vertices.Count;
            if (countX < 2) return Meshie.Empty();
            var countY = pathieY.Vertices.Count;
            if (countY < 2) return Meshie.Empty();

            var vertices = pathieY.Vertices.SelectMany(pY => pathieX.Vertices.Select(pX =>
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
            var gons = new List<Gon>();

            var vertiesXWithIndex = pathieX.Vertices.Select((v, i) => (v, i)).ToList();
            if (pathieX.isLoop) vertiesXWithIndex.Add(vertiesXWithIndex.First());

            var vertiesYWithIndex = pathieY.Vertices.Select((v, i) => (v, i)).ToList();
            if (pathieY.isLoop) vertiesYWithIndex.Add(vertiesYWithIndex.First());

            var a = new int[4];
            for (int y = 0, yi = 1; yi < vertiesYWithIndex.Count; y++, yi++)
            {
                var ya = vertiesYWithIndex[y]; 
                var yb = vertiesYWithIndex[yi]; 
                if (ya.v.TranslationEquals(yb.v, 0f)) continue;
                for (int x = 0, xi = 1; xi < vertiesXWithIndex.Count; x++, xi++)
                {
                    var xa = vertiesXWithIndex[x]; 
                    var xb = vertiesXWithIndex[xi]; 
                    if (xa.v.TranslationEquals(xb.v, 0f)) continue;

                    a[0] = xa.i + ya.i * countX;
                    a[1] = xb.i + ya.i * countX;
                    a[2] = xa.i + yb.i * countX;
                    a[3] = xb.i + yb.i * countX;

                    var cellPattern = _defaultCellPatternKind;
                    var cellMaterialIndex = _materialIndex;
                    foreach (var o in _cellPatternOverrides)
                    {
                        if (x < o.cellRange.xMin || x >= o.cellRange.xMax || y < o.cellRange.yMin || y >= o.cellRange.yMax) continue;
                        cellPattern = o.cellPatternKind;
                        if (o.materialIndex > -1) cellMaterialIndex = o.materialIndex;
                        break;
                    }

                    CellFormKind cellForm;
                    switch (cellPattern)
                    {
                        case CellPatternKind.Default:
                        case CellPatternKind.AllRight:
                        case CellPatternKind.AllLeft:
                        case CellPatternKind.TriangleBottomLeft:
                        case CellPatternKind.TriangleBottomRight:
                        case CellPatternKind.TriangleTopLeft:
                        case CellPatternKind.TriangleTopRight:
                        case CellPatternKind.None:
                            cellForm = (CellFormKind)cellPattern;
                            break;
                        case CellPatternKind.CheckeredRight:
                            cellForm = ((x ^ y) & 1) == 0 ? CellFormKind.Right : CellFormKind.Left;
                            break;
                        case CellPatternKind.CheckeredLeft:
                            cellForm = ((x ^ y) & 1) == 0 ? CellFormKind.Left : CellFormKind.Right;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    gons.AddRange(CellIndices[cellForm].Select(cellIndices => new Gon(cellIndices.Select(i => a[i]).ToArray(), cellMaterialIndex)));
                }
            }

            return Meshie.Builder(vertices, gons, true)
                .ToMeshie(_defaultCellPatternKind == CellPatternKind.None || _cellPatternOverrides.Length > 0);
        }

        public Pathie Extract(string pathName, LodMaskLayer lod)
        {
            Pathie DoExtractX(Pathie pathieX, Vertie vertexY)
            {
                return DoExtract2(pathieX.Vertices, Enumerable.Repeat(vertexY, pathieX.Vertices.Count), pathieX.isLoop);
            }

            Pathie DoExtractY(Vertie vertexX, Pathie pathieY)
            {
                return DoExtract2(Enumerable.Repeat(vertexX, pathieY.Vertices.Count), pathieY.Vertices, pathieY.isLoop);
            }

            Pathie DoExtract2(IEnumerable<Vertie> verticesX, IEnumerable<Vertie> verticesY, bool isLoop)
            {
                return new Pathie(verticesX.Zip(verticesY, (pX, pY) =>
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
                }), isLoop);
            }

            switch (pathName)
            {
                case PathNames.XFirst:
                    return DoExtractY(_pathieX.Build(lod).Vertices.First(), _pathieY.Build(lod));
                case PathNames.XLast:
                    return DoExtractY(_pathieX.Build(lod).Vertices.Last(), _pathieY.Build(lod));
                case PathNames.YFirst:
                    return DoExtractX(_pathieX.Build(lod), _pathieY.Build(lod).Vertices.First());
                case PathNames.YLast:
                    return DoExtractX(_pathieX.Build(lod), _pathieY.Build(lod).Vertices.Last());
                case PathNames.X:
                    return _pathieX.Build(lod);
                case PathNames.Y:
                    return _pathieY.Build(lod);
                default:
                    return Pathie.Empty();
            }
        }

        public static class PathNames
        {
            public const string X = "X";
            public const string Y = "Y";
            public const string XFirst = "X.First";
            public const string XLast = "X.Last";
            public const string YFirst = "Y.First";
            public const string YLast = "Y.Last";
        }

        public enum OperatorKind
        {
            TranslateOnly = 0,
            ApplyX = 1,
            ApplyY = 2
        }

        public enum CellPatternKind
        {
            Default = 0,
            AllRight = 2,
            AllLeft = 3,
            CheckeredRight = 4,
            CheckeredLeft = 5,
            TriangleBottomLeft = 128,
            TriangleBottomRight = 129,
            TriangleTopLeft = 130,
            TriangleTopRight = 131,
            None = -1
        }

        [Serializable]
        public class CellOverride
        {
            public CellPatternKind cellPatternKind;
            [RectIntCustom]
            public RectInt cellRange = new RectInt(0, 0, 1, 1);

            public int materialIndex;
        }

        enum CellFormKind
        {
            Default = 0,
            Right = 2,
            Left = 3,
            TriangleBottomLeft = 128,
            TriangleBottomRight = 129,
            TriangleTopLeft = 130,
            TriangleTopRight = 131,
            None = -1
        }
    }
}