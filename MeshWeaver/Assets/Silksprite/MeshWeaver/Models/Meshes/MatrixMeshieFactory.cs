using System;
using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshWeaver.Models.Extensions;
using Silksprite.MeshWeaver.Models.Modifiers;
using Silksprite.MeshWeaver.Models.Paths;

namespace Silksprite.MeshWeaver.Models.Meshes
{
    public class MatrixMeshieFactory : IMeshieFactory
    {
        readonly IPathieFactory _pathieX;
        readonly IPathieFactory _pathieY;

        readonly OperatorKind _operatorKind;
        readonly CellPatternKind _defaultCellPatternKind;
        readonly List<CellPatternOverride> _cellPatternOverrides;
        readonly int _materialIndex;

        static readonly Dictionary<CellFormKind, IEnumerable<int>> CellIndices = new Dictionary<CellFormKind, IEnumerable<int>>
        {
            [CellFormKind.Default] = new[] { 0, 2, 3, 0, 3, 1 },
            [CellFormKind.Right] = new[] { 0, 2, 3, 0, 3, 1 }, 
            [CellFormKind.Left] = new[] { 0, 2, 1, 1, 2, 3 }, 
            [CellFormKind.TriangleBottomLeft] = new[] { 0, 2, 1 }, 
            [CellFormKind.TriangleBottomRight] = new[] { 0, 3, 1 }, 
            [CellFormKind.TriangleTopLeft] = new[] { 0, 2, 3 }, 
            [CellFormKind.TriangleTopRight] = new[] { 1, 2, 3 }, 
            [CellFormKind.None] = Array.Empty<int>() 
        };

        public MatrixMeshieFactory(IPathieFactory pathieX, IPathieFactory pathieY,
            OperatorKind operatorKind, CellPatternKind defaultCellPatternKind, List<CellPatternOverride> cellPatternOverrides,
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

            var activeX = pathieX.Active;
            var countX = activeX.Vertices.Count;
            if (countX < 2) return Meshie.Empty();
            var activeY = pathieY.Active;
            var countY = activeY.Vertices.Count;
            if (countY < 2) return Meshie.Empty();

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

            var vertiesXWithIndex = pathieX.Active.Vertices.Select((v, i) => (v, i)).ToList();
            if (pathieX.isLoop) vertiesXWithIndex.Add(vertiesXWithIndex.First());
            var indexPairsX = vertiesXWithIndex
                .Pairwise((a, b) => (a: a.v, b: b.v, i: (a: a.i, b: b.i)))
                .Where(ab => !ab.a.TranslationEquals(ab.b, 0f))
                .Select(ab => ab.i);

            var vertiesYWithIndex = pathieY.Active.Vertices.Select((v, i) => (v, i)).ToList();
            if (pathieY.isLoop) vertiesYWithIndex.Add(vertiesYWithIndex.First());
            var indexPairsY = vertiesYWithIndex
                .Pairwise((a, b) => (a: a.v, b: b.v, i: (a: a.i, b: b.i)))
                .Where(ab => !ab.a.TranslationEquals(ab.b, 0f))
                .Select(ab => ab.i);
            
            var indices = indexPairsY.SelectMany(y =>
                {
                    return indexPairsX.Select(x =>
                    {
                        var a = new[]
                        {
                            x.a + y.a * countX,
                            x.b + y.a * countX,
                            x.a + y.b * countX,
                            x.b + y.b * countX 
                        };
                        return (x: x.a, y: y.a, a);
                    });
                }).SelectMany(xya =>
                {
                    CellPatternKind CellKindAt(int x, int y)
                    {
                        foreach (var o in _cellPatternOverrides)
                        {
                            if (x >= o.xMin && x <= o.xMax && y >= o.yMin && y <= o.yMax) return o.cellPatternKind;
                        }
                        return _defaultCellPatternKind;
                    }

                    var cellPattern = CellKindAt(xya.x, xya.y);
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
                            cellForm = ((xya.x ^ xya.y) & 1) == 0 ? CellFormKind.Right : CellFormKind.Left; 
                            break;
                        case CellPatternKind.CheckeredLeft:
                            cellForm = ((xya.x ^ xya.y) & 1) == 0 ? CellFormKind.Left : CellFormKind.Right; 
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    return CellIndices[cellForm].Select(i => xya.a[i]);
                });
            return Meshie.Builder(vertices, indices, _materialIndex, true)
                .ToMeshie(_defaultCellPatternKind == CellPatternKind.None || _cellPatternOverrides.Count > 0);
        }

        public Pathie Extract(string pathName, LodMaskLayer lod)
        {
            Pathie DoExtract(IPathieFactory longitude, IPathieFactory latitude, bool isEnd)
            {
                var ends = longitude.Build(lod);
                var translation = isEnd ? ends.Last : ends.First;
                return latitude.WithModifiers(new VertwiseTranslate(translation.Translation)).Build(lod);
            }

            switch (pathName)
            {
                case PathNames.XFirst:
                    return DoExtract(_pathieY, _pathieX, false);
                case PathNames.XLast:
                    return DoExtract(_pathieY, _pathieX, true);
                case PathNames.YFirst:
                    return DoExtract(_pathieX, _pathieY, false);
                case PathNames.YLast:
                    return DoExtract(_pathieX, _pathieY, true);
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
        public struct CellPatternOverride
        {
            public CellPatternKind cellPatternKind;
            public int xMin;
            public int xMax;
            public int yMin;
            public int yMax;
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