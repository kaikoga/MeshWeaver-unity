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
        readonly LongitudeAxisKind _longitudeAxisKind;

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
            MatrixMeshieFactory.OperatorKind operatorKind, LongitudeAxisKind longitudeAxisKind,
            bool fillBody, bool fillBottom, bool fillTop,
            int uvChannelBody, int uvChannelBottom, int uvChannelTop,
            int materialIndexBody, int materialIndexBottom, int materialIndexTop)
        {
            _pathieX = pathieX;
            _pathieY = pathieY;
            _operatorKind = operatorKind;
            _longitudeAxisKind = longitudeAxisKind;
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
                builder.Concat(new MatrixMeshieFactory(_pathieX, _pathieY, _operatorKind, _materialIndexBody).Build(lod), Matrix4x4.identity, _uvChannelBody);
            }

            var (longitudePathie, latitudePathie) = _longitudeAxisKind == LongitudeAxisKind.Y ? (_pathieY, _pathieX) : (_pathieX, _pathieY);
            var ends = longitudePathie.Build(lod);

            if (_fillBottom)
            {
                builder.Concat(new PolygonMeshieFactory2(latitudePathie, _materialIndexBottom).Build(lod), ends.First.Translation, _uvChannelBottom);
            }
            if (_fillTop)
            {
                builder.Concat(new PolygonMeshieFactory2(latitudePathie, _materialIndexTop).Build(lod).Apply(MeshReverse.BackOnly), ends.Last.Translation, _uvChannelTop);
            }
            return builder.ToMeshie();
        }
        
        public enum LongitudeAxisKind
        {
            X = 1,
            Y = 2
        }
    }
}