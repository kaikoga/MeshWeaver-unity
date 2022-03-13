using System.Linq;
using Silksprite.MeshBuilder.Models.Meshes.Modifiers;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models.Meshes
{
    public class PillarMeshieFactory : IMeshieFactory<Pathie, Pathie>
    {
        readonly bool _fillBody;
        readonly bool _fillBottom;
        readonly bool _fillTop;

        public PillarMeshieFactory(bool fillBody, bool fillBottom, bool fillTop)
        {
            _fillBody = fillBody;
            _fillBottom = fillBottom;
            _fillTop = fillTop;
        }
        public Meshie Build(Pathie pathieX, Pathie pathieY)
        {
            var meshie = new Meshie();
            if (_fillBody)
            {
                meshie.Concat(new MatrixMeshieFactory().Build(pathieX, pathieY), Matrix4x4.identity);
            }
            if (_fillBottom)
            {
                meshie.Concat(new PolygonMeshieFactory2().Build(pathieX), Matrix4x4.Translate(pathieY.First().Vertex));
            }
            if (_fillTop)
            {
                meshie.Concat(MeshReverse.BackOnly.Modify(new PolygonMeshieFactory2().Build(pathieX)), Matrix4x4.Translate(pathieY.Last().Vertex));
            }
            return meshie;
        }
    }
}