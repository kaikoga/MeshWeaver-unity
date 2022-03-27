using Silksprite.MeshBuilder.Models.Meshes.Modifiers;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models.Meshes
{
    public class PillarMeshieFactory : IMeshieFactory
    {
        readonly Pathie _pathieX;
        readonly Pathie _pathieY;
        
        readonly bool _fillBody;
        readonly bool _fillBottom;
        readonly bool _fillTop;

        readonly int _uvChannelBody;
        readonly int _uvChannelBottom;
        readonly int _uvChannelTop;
        
        public PillarMeshieFactory(Pathie pathieX, Pathie pathieY, bool fillBody, bool fillBottom, bool fillTop, int uvChannelBody, int uvChannelBottom, int uvChannelTop)
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

        public Meshie Build()
        {
            var builder = Meshie.Builder();
            if (_fillBody)
            {
                builder.Concat(new MatrixMeshieFactory(_pathieX, _pathieY).Build(), Matrix4x4.identity, _uvChannelBody);
            }
            if (_fillBottom)
            {
                builder.Concat(new PolygonMeshieFactory2(_pathieX).Build(), Matrix4x4.Translate(_pathieY.First.Vertex), _uvChannelBottom);
            }
            if (_fillTop)
            {
                builder.Concat(new PolygonMeshieFactory2(_pathieX).Build().Apply(MeshReverse.BackOnly), Matrix4x4.Translate(_pathieY.Last.Vertex), _uvChannelTop);
            }
            return builder.ToMeshie();
        }
    }
}