using System.Collections.Generic;
using Silksprite.MeshWeaver.Models.Extensions;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models.Paths
{
    public class VertexFactory : IPathieFactory
    {
        public static readonly VertexFactory Default = new VertexFactory();

        readonly LodMask _lodMask;
        readonly bool _crease;
        readonly Mux<Vector2> _uvs;

        public VertexFactory(LodMask lodMask = LodMask.All, bool crease = false, Mux<Vector2> uvs = null)
        {
            _lodMask = lodMask;
            _crease = crease;
            _uvs = uvs ?? Mux.Empty<Vector2>();
        }

        public Pathie Build(LodMaskLayer lod)
        {
            Vertie CreateVertie() => new Vertie(Matrix4x4.identity, !_lodMask.HasLayer(lod), _uvs);

            IEnumerable<Vertie> CreateVerties()
            {
                yield return CreateVertie();
                yield return CreateVertie();
            }

            if (_crease)
            {
                return new Pathie(CreateVerties(), true);
            }
            else
            {
                return new Pathie(CreateVertie());
            }
        }
    }
}