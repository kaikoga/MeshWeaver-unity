using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models.Meshes
{
    public class CompositeMeshieFactory : IMeshieFactory
    {
        readonly IMeshieFactory[] _children;

        public CompositeMeshieFactory(IEnumerable<IMeshieFactory> children)
        {
            _children = children.ToArray();
        }

        public Meshie Build(LodMaskLayer lod)
        {
            return _children.Aggregate(Meshie.Builder(), (builder, factory) => builder.Concat(factory.Build(lod), Matrix4x4.identity, 0)).ToMeshie();
        }
    }
}