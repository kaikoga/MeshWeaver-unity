using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models.Paths
{
    public class CompositePathieFactory : IPathieFactory
    {
        readonly ChildPathieFactory[] _children;
        readonly bool _isLoop;
        readonly bool _smoothJoin;

        CompositePathieFactory(ChildPathieFactory[] children, bool isLoop, bool smoothJoin)
        {
            _children = children;
            _isLoop = isLoop;
            _smoothJoin = smoothJoin;
        }

        CompositePathieFactory(IEnumerable<ChildPathieFactory> children, bool isLoop, bool smoothJoin) : this(children.ToArray(), isLoop, smoothJoin) { }

        public Pathie Build(LodMaskLayer lod)
        {
            return _children.Aggregate(Pathie.Builder(_isLoop, _smoothJoin), (builder, factory) => builder.Concat(factory.PathieFactory.Build(lod), factory.Translation)).ToPathie();
        }

        public static CompositePathieFactoryBuilder Builder(bool isLoop, bool smoothJoin = false) => new CompositePathieFactoryBuilder(isLoop, smoothJoin);

        class ChildPathieFactory
        {
            public IPathieFactory PathieFactory;
            public Matrix4x4 Translation;
        }

        public class CompositePathieFactoryBuilder
        {
            readonly List<ChildPathieFactory> _children = new List<ChildPathieFactory>();
            readonly bool _isLoop;
            readonly bool _smoothJoin;

            public CompositePathieFactoryBuilder(bool isLoop, bool smoothJoin = false)
            {
                _isLoop = isLoop;
                _smoothJoin = smoothJoin;
            }

            public CompositePathieFactoryBuilder Concat(IPathieFactory pathieFactory, Matrix4x4 translation)
            {
                _children.Add(new ChildPathieFactory
                {
                    PathieFactory = pathieFactory,
                    Translation = translation
                });
                return this;
            }

            public CompositePathieFactoryBuilder ConcatVertex(Matrix4x4 translation) => Concat(VertexFactory.Default, translation);
            public CompositePathieFactoryBuilder ConcatVertex(Vector3 vertex) => Concat(VertexFactory.Default, Matrix4x4.Translate(vertex));

            public CompositePathieFactory ToFactory() => new CompositePathieFactory(_children, _isLoop, _smoothJoin);
        }
    }
}