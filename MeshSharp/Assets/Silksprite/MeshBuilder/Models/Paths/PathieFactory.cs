using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshBuilder.Models.Base;

namespace Silksprite.MeshBuilder.Models.Paths
{
    public static class PathieFactory
    {
        public static readonly IPathieFactory Empty = new EmptyPathieFactory();

        class EmptyPathieFactory : IPathieFactory
        {
            public Pathie Build(LodMaskLayer lod) => Pathie.Empty();
        }
    }

    public class ModifiedPathieFactory : IPathieFactory
    {
        readonly IPathieFactory _factory;
        readonly ChildModifier[] _modifiers;

        ModifiedPathieFactory(IPathieFactory factory, IEnumerable<ChildModifier> modifiers)
        {
            _factory = factory;
            _modifiers = modifiers.ToArray();
        }

        public Pathie Build(LodMaskLayer lod)
        {
            return _modifiers.Aggregate(_factory.Build(lod), (current, modifier) => modifier.Modifier.Modify(current));
        }
            
        public static ModifiedPathieFactoryBuilder Builder(IPathieFactory factory) => new ModifiedPathieFactoryBuilder(factory);

        class ChildModifier
        {
            public readonly IPathieModifier Modifier;

            public ChildModifier(IPathieModifier modifier)
            {
                Modifier = modifier;
            }
        }

        public class ModifiedPathieFactoryBuilder
        {
            readonly IPathieFactory _factory;
            readonly List<ChildModifier> _children = new List<ChildModifier>();

            public ModifiedPathieFactoryBuilder(IPathieFactory factory)
            {
                _factory = factory;
            }

            public ModifiedPathieFactoryBuilder Concat(IPathieModifier modifier)
            {
                _children.Add(new ChildModifier(modifier));
                return this;
            }

            public ModifiedPathieFactory ToFactory() => new ModifiedPathieFactory(_factory, _children);
        }
    }
}