using Silksprite.MeshBuilder.Extensions;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models.Paths
{
    public class VertexFactory : IPathieFactory
    {
        readonly LodMask _lodMask;
        readonly Mux<Vector2> _uvs;

        public VertexFactory(LodMask lodMask, Mux<Vector2> uvs)
        {
            _lodMask = lodMask;
            _uvs = uvs;
        }

        public Pathie Build(LodMaskLayer lod)
        {
            return new Pathie(new Vertie(Matrix4x4.identity, !_lodMask.HasLayer(lod), _uvs));
        }
    }
}