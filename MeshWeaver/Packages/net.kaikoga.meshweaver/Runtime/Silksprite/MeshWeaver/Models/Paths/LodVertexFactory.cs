using System.Collections.Generic;
using Silksprite.MeshWeaver.Models.Extensions;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models.Paths
{
    public class LodVertexFactory : IPathieFactory
    {
        public static readonly LodVertexFactory Default = new LodVertexFactory(LodMask.All);

        readonly LodMask _lodMask;
        readonly bool _crease;
        readonly Mux<Vector2> _uvs;

        public LodVertexFactory(LodMask lodMask, bool crease = false, Mux<Vector2> uvs = null)
        {
            _lodMask = lodMask;
            _crease = crease;
            _uvs = uvs ?? Mux.Empty<Vector2>();
        }

        public Pathie Build(LodMaskLayer lod)
        {
            Vertie CreateVertie() => new Vertie(Matrix4x4.identity, _uvs);

            IEnumerable<Vertie> CreateVerties()
            {
                yield return CreateVertie();
                yield return CreateVertie();
            }

            if (!_lodMask.HasLayer(lod)) return Pathie.Empty();
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