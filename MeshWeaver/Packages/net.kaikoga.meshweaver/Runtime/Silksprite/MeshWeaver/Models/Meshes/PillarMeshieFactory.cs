using System;
using Silksprite.MeshWeaver.Models.Meshes.Modifiers;
using Silksprite.MeshWeaver.Models.Paths;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models.Meshes
{
    public class PillarMeshieFactory : IMeshieFactory
    {
        readonly IPathieFactory _pathieX;
        readonly IPathieFactory _pathieY;

        readonly MatrixMeshieFactory.OperatorKind _operatorKind;
        readonly MatrixMeshieFactory.CellPatternKind _defaultCellPatternKind;
        readonly MatrixMeshieFactory.CellOverride[] _cellPatternOverrides;
        readonly LongitudeAxisKind _longitudeAxisKind;
        readonly bool _reverseLids;

        readonly bool _fillBody;
        readonly bool _fillBottom;
        readonly bool _fillTop;

        readonly int _uvChannelBody;
        readonly int _uvChannelBottom;
        readonly int _uvChannelTop;
        
        readonly int _materialIndexBody;
        readonly int _materialIndexBottom;
        readonly int _materialIndexTop;

        public PillarMeshieFactory(IPathieFactory pathieX, IPathieFactory pathieY,
            MatrixMeshieFactory.OperatorKind operatorKind, MatrixMeshieFactory.CellPatternKind defaultCellPatternKind, MatrixMeshieFactory.CellOverride[] cellPatternOverrides,
            LongitudeAxisKind longitudeAxisKind, bool reverseLids,
            bool fillBody, bool fillBottom, bool fillTop,
            int uvChannelBody, int uvChannelBottom, int uvChannelTop,
            int materialIndexBody, int materialIndexBottom, int materialIndexTop)
        {
            _pathieX = pathieX;
            _pathieY = pathieY;
            _operatorKind = operatorKind;
            _defaultCellPatternKind = defaultCellPatternKind;
            _cellPatternOverrides = cellPatternOverrides;
            _longitudeAxisKind = longitudeAxisKind;
            _reverseLids = reverseLids;
            _fillBody = fillBody;
            _fillBottom = fillBottom;
            _fillTop = fillTop;
            _uvChannelBody = uvChannelBody;
            _uvChannelBottom = uvChannelBottom;
            _uvChannelTop = uvChannelTop;
            _materialIndexBody = materialIndexBody;
            _materialIndexBottom = materialIndexBottom;
            _materialIndexTop = materialIndexTop;
        }

        public Meshie Build(LodMaskLayer lod)
        {
            var builder = Meshie.Builder();
            if (_fillBody)
            {
                builder.Concat(new MatrixMeshieFactory(_pathieX, _pathieY, _operatorKind, _defaultCellPatternKind, _cellPatternOverrides, _materialIndexBody).Build(lod), Matrix4x4.identity, _uvChannelBody);
            }

            if (_fillBottom)
            {
                string BottomPathName()
                {
                    switch (_longitudeAxisKind)
                    {
                        case LongitudeAxisKind.X:
                            return MatrixMeshieFactory.PathNames.XFirst;
                        case LongitudeAxisKind.Y:
                            return MatrixMeshieFactory.PathNames.YFirst;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                var bottomMeshie = new PolygonMeshieFactory2(new ExtractPathieFactory(this, BottomPathName()), _materialIndexBottom).Build(lod);
                if (_reverseLids) bottomMeshie = bottomMeshie.Apply(MeshReverse.BackOnly);
                builder.Concat(bottomMeshie, Matrix4x4.identity, _uvChannelBottom);
            }

            if (_fillTop)
            {
                string TopPathName()
                {
                    switch (_longitudeAxisKind)
                    {
                        case LongitudeAxisKind.X:
                            return MatrixMeshieFactory.PathNames.XLast;
                        case LongitudeAxisKind.Y:
                            return MatrixMeshieFactory.PathNames.YLast;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                var topMeshie = new PolygonMeshieFactory2(new ExtractPathieFactory(this, TopPathName()), _materialIndexTop).Build(lod);
                if (!_reverseLids) topMeshie = topMeshie.Apply(MeshReverse.BackOnly);
                builder.Concat(topMeshie, Matrix4x4.identity, _uvChannelTop);
            }

            return builder.ToMeshie();
        }

        public Pathie Extract(string pathName, LodMaskLayer lod)
        {
            return new MatrixMeshieFactory(_pathieX, _pathieY, _operatorKind, _defaultCellPatternKind, _cellPatternOverrides, _materialIndexBody).Extract(pathName, lod);
        }

        public enum LongitudeAxisKind
        {
            X = 1,
            Y = 2
        }
    }
}