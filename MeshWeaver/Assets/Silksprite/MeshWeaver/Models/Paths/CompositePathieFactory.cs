using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models.Paths
{
    public class CompositePathieFactory : IPathieFactory
    {
        readonly ChildPathieFactory[] _children;
        readonly bool _isLoop;

        CompositePathieFactory(ChildPathieFactory[] children, bool isLoop)
        {
            _children = children;
            _isLoop = isLoop;
        }

        CompositePathieFactory(IEnumerable<ChildPathieFactory> children, bool isLoop) : this(children.ToArray(), isLoop) { }

        public Pathie Build(LodMaskLayer lod)
        {
            return _children.Aggregate(Pathie.Builder(_isLoop), (builder, factory) => builder.Concat(factory.PathieFactory.Build(lod), factory.Translation)).ToPathie();
        }

        public static CompositePathieFactoryBuilder Builder(bool isLoop) => new CompositePathieFactoryBuilder(isLoop);

        class ChildPathieFactory
        {
            public IPathieFactory PathieFactory;
            public Matrix4x4 Translation;
        }

        public class CompositePathieFactoryBuilder
        {
            readonly List<ChildPathieFactory> _children = new List<ChildPathieFactory>();
            readonly bool _isLoop;

            public CompositePathieFactoryBuilder(bool isLoop) => _isLoop = isLoop;

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

            public CompositePathieFactory ToFactory() => new CompositePathieFactory(_children, _isLoop);
        }
    }
}