using Silksprite.MeshBuilder.Models.Extensions;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models.Paths
{
    public class VertexFactory : IPathieFactory
    {
        public static readonly VertexFactory Default = new VertexFactory();

        readonly LodMask _lodMask;
        readonly Mux<Vector2> _uvs;

        public VertexFactory(LodMask lodMask = LodMask.All, Mux<Vector2> uvs = null)
        {
            _lodMask = lodMask;
            _uvs = uvs ?? Mux.Empty<Vector2>();
        }

        public Pathie Build(LodMaskLayer lod)
        {
            return new Pathie(new Vertie(Matrix4x4.identity, !_lodMask.HasLayer(lod), _uvs));
        }
    }
}