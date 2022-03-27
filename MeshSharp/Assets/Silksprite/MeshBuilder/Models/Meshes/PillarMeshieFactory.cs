using Silksprite.MeshBuilder.Models.Meshes.Modifiers;
using Silksprite.MeshBuilder.Models.Paths;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models.Meshes
{
    public class PillarMeshieFactory : IMeshieFactory
    {
        readonly IPathieFactory _pathieX;
        readonly IPathieFactory _pathieY;
        
        readonly bool _fillBody;
        readonly bool _fillBottom;
        readonly bool _fillTop;

        readonly int _uvChannelBody;
        readonly int _uvChannelBottom;
        readonly int _uvChannelTop;
        
        public PillarMeshieFactory(IPathieFactory pathieX, IPathieFactory pathieY, bool fillBody, bool fillBottom, bool fillTop, int uvChannelBody, int uvChannelBottom, int uvChannelTop)
        {
            _pathieX = pathieX;
            _pathieY = pathieY;
            _fillBody = fillBody;
            _fillBottom = fillBottom;
            _fillTop = fillTop;
            _uvChannelBody = uvChannelBody;
            _uvChannelBottom = uvChannelBottom;
            _uvChannelTop = uvChannelTop;
        }

        public Meshie Build(LodMaskLayer lod)
        {
            var pathieY = _pathieX.Build(lod);
            var builder = Meshie.Builder();
            if (_fillBody)
            {
                builder.Concat(new MatrixMeshieFactory(_pathieX, _pathieY).Build(lod), Matrix4x4.identity, _uvChannelBody);
            }
            if (_fillBottom)
            {
                builder.Concat(new PolygonMeshieFactory2(_pathieX).Build(lod), Matrix4x4.Translate(pathieY.First.Vertex), _uvChannelBottom);
            }
            if (_fillTop)
            {
                builder.Concat(new PolygonMeshieFactory2(_pathieX).Build(lod).Apply(MeshReverse.BackOnly), Matrix4x4.Translate(pathieY.Last.Vertex), _uvChannelTop);
            }
            return builder.ToMeshie();
        }
    }
}