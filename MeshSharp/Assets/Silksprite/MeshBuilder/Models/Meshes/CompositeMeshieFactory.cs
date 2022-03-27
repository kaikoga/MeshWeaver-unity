using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models.Meshes
{
    public class CompositeMeshieFactory : IMeshieFactory
    {
        readonly ChildMeshieFactory[] _children;

        CompositeMeshieFactory(ChildMeshieFactory[] children)
        {
            _children = children;
        }

        CompositeMeshieFactory(IEnumerable<ChildMeshieFactory> children) : this(children.ToArray()) { }

        public Meshie Build(LodMaskLayer lod)
        {
            return _children.Aggregate(Meshie.Builder(), (builder, factory) => builder.Concat(factory.MeshieFactory.Build(lod), factory.Translation, 0)).ToMeshie();
        }

        public static CompositeMeshieFactory Empty() => new CompositeMeshieFactory(Array.Empty<ChildMeshieFactory>());
        public static CompositeMeshieFactoryBuilder Builder() => new CompositeMeshieFactoryBuilder();

        class ChildMeshieFactory
        {
            public IMeshieFactory MeshieFactory;
            public Matrix4x4 Translation;
        }

        public class CompositeMeshieFactoryBuilder
        {
            readonly List<ChildMeshieFactory> _children = new List<ChildMeshieFactory>();

            public CompositeMeshieFactoryBuilder Concat(IMeshieFactory meshieFactory, Matrix4x4 translation)
            {
                _children.Add(new ChildMeshieFactory
                {
                    MeshieFactory = meshieFactory,
                    Translation = translation
                });
                return this;
            }

            public CompositeMeshieFactory ToFactory() => new CompositeMeshieFactory(_children);
        }
    }
}