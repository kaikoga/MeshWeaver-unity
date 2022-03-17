using Silksprite.MeshBuilder.Models.Meshes.Modifiers;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models.Meshes
{
    public class PillarMeshieFactory : IMeshieFactory<Pathie, Pathie>
    {
        readonly bool _fillBody;
        readonly bool _fillBottom;
        readonly bool _fillTop;

        readonly int _uvBody;
        readonly int _uvBottom;
        readonly int _uvTop;

        public PillarMeshieFactory(bool fillBody, bool fillBottom, bool fillTop, int uvBody, int uvBottom, int uvTop)
        {
            _fillBody = fillBody;
            _fillBottom = fillBottom;
            _fillTop = fillTop;
            _uvBody = uvBody;
            _uvBottom = uvBottom;
            _uvTop = uvTop;
        }
        public Meshie Build(Pathie pathieX, Pathie pathieY)
        {
            var meshie = new Meshie();
            if (_fillBody)
            {
                meshie.Concat(new MatrixMeshieFactory().Build(pathieX, pathieY), Matrix4x4.identity, _uvBody);
            }
            if (_fillBottom)
            {
                meshie.Concat(new PolygonMeshieFactory2().Build(pathieX), Matrix4x4.Translate(pathieY.First.Vertex), _uvBottom);
            }
            if (_fillTop)
            {
                meshie.Concat(new PolygonMeshieFactory2().Build(pathieX).Apply(MeshReverse.BackOnly), Matrix4x4.Translate(pathieY.Last.Vertex), _uvTop);
            }
            return meshie;
        }
    }
}