using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models.Paths
{
    public class CompositePathieFactory : IPathieFactory
    {
        readonly IPathieFactory[] _children;

        public CompositePathieFactory(IEnumerable<IPathieFactory> children)
        {
            _children = children.ToArray();
        }

        public Pathie Build(LodMaskLayer lod)
        {
            return _children.Aggregate(Pathie.Builder(), (builder, factory) => builder.Concat(factory.Build(lod), Matrix4x4.identity)).ToPathie();
        }
    }
}