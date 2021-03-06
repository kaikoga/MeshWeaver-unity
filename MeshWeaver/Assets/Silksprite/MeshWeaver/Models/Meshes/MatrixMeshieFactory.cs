using System;
using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshWeaver.CustomDrawers;
using Silksprite.MeshWeaver.Models.Extensions;
using Silksprite.MeshWeaver.Models.Modifiers;
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
        readonly List<CellOverride> _cellPatternOverrides;
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
            OperatorKind operatorKind, CellPatternKind defaultCellPatternKind, List<CellOverride> cellPatternOverrides,
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
            var gons = new List<Gon>();

            var vertiesXWithIndex = pathieX.Active.Vertices.Select((v, i) => (v, i)).ToList();
            if (pathieX.isLoop) vertiesXWithIndex.Add(vertiesXWithIndex.First());

            var vertiesYWithIndex = pathieY.Active.Vertices.Select((v, i) => (v, i)).ToList();
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