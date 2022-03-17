using Silksprite.MeshBuilder.Models.Meshes.Modifiers;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models.Meshes
{
    public class PillarMeshieFactory : IMeshieFactory<Pathie, Pathie>
    {
        readonly bool _fillBody;
        readonly bool _fillBottom;
        readonly bool _fillTop;

        readonly int _uvChannelBody;
        readonly int _uvChannelBottom;
        readonly int _uvChannelTop;

        public PillarMeshieFactory(bool fillBody, bool fillBottom, bool fillTop, int uvChannelBody, int uvChannelBottom, int uvChannelTop)
        {
            _fillBody = fillBody;
            _fillBottom = fillBottom;
            _fillTop = fillTop;
            _uvChannelBody = uvChannelBody;
            _uvChannelBottom = uvChannelBottom;
            _uvChannelTop = uvChannelTop;
        }
        public Meshie Build(Pathie pathieX, Pathie pathieY)
        {
            var meshie = new Meshie();
            if (_fillBody)
            {
                meshie.Concat(new MatrixMeshieFactory().Build(pathieX, pathieY), Matrix4x4.identity, _uvChannelBody);
            }
            if (_fillBottom)
            {
                meshie.Concat(new PolygonMeshieFactory2().Build(pathieX), Matrix4x4.Translate(pathieY.First.Vertex), _uvChannelBottom);
            }
            if (_fillTop)
            {
                meshie.Concat(new PolygonMeshieFactory2().Build(pathieX).Apply(MeshReverse.BackOnly), Matrix4x4.Translate(pathieY.Last.Vertex), _uvChannelTop);
            }
            return meshie;
        }
    }
}