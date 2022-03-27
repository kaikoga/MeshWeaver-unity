using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models.Paths
{
    public class CompositePathieFactory : IPathieFactory
    {
        readonly ChildPathieFactory[] _children;

        CompositePathieFactory(ChildPathieFactory[] children)
        {
            _children = children;
        }

        public CompositePathieFactory(IEnumerable<ChildPathieFactory> children) : this(children.ToArray()) { }

        public Pathie Build(LodMaskLayer lod)
        {
            return _children.Aggregate(Pathie.Builder(), (builder, factory) => builder.Concat(factory.PathieFactory.Build(lod), factory.Translation)).ToPathie();
        }
        
        public static CompositePathieFactory Empty() => new CompositePathieFactory(Array.Empty<ChildPathieFactory>());
        public static CompositePathieFactoryBuilder Builder() => new CompositePathieFactoryBuilder();

        public class ChildPathieFactory
        {
            public IPathieFactory PathieFactory;
            public Matrix4x4 Translation;
        }

        public class CompositePathieFactoryBuilder
        {
            readonly List<ChildPathieFactory> _children = new List<ChildPathieFactory>();

            public CompositePathieFactoryBuilder Concat(IPathieFactory pathieFactory, Matrix4x4 translation)
            {
                _children.Add(new ChildPathieFactory
                {
                    PathieFactory = pathieFactory,
                    Translation = translation
                });
                return this;
            }

            public CompositePathieFactory ToFactory() => new CompositePathieFactory(_children);
        }
    }
}