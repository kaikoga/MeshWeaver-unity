using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models.Paths
{
    public class CompositePathieFactory : IPathieFactory
    {
        readonly ChildPathieFactory[] _children;

        CompositePathieFactory(ChildPathieFactory[] children)
        {
            _children = children;
        }

        CompositePathieFactory(IEnumerable<ChildPathieFactory> children) : this(children.ToArray()) { }

        public Pathie Build(LodMaskLayer lod)
        {
            return _children.Aggregate(Pathie.Builder(), (builder, factory) => builder.Concat(factory.PathieFactory.Build(lod), factory.Translation)).ToPathie();
        }

        public static CompositePathieFactoryBuilder Builder() => new CompositePathieFactoryBuilder();

        class ChildPathieFactory
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

            public CompositePathieFactoryBuilder ConcatVertex(Matrix4x4 translation)
            {
                return Concat(VertexFactory.Default, translation);
            }

            public CompositePathieFactory ToFactory() => new CompositePathieFactory(_children);
        }
    }
}