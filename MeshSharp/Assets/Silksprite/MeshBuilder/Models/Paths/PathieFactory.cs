using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshBuilder.Models.Extensions;
using Silksprite.MeshBuilder.Models.Paths.Modifiers;

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
            var result = _factory.Build(lod);
            return _modifiers.Where(modifier => modifier.LodMask.HasLayer(lod))
                .Aggregate(result, (current, modifier) => modifier.Modifier.Modify(current));
        }
            
        public static ModifiedPathieFactoryBuilder Builder(IPathieFactory factory) => new ModifiedPathieFactoryBuilder(factory);

        class ChildModifier
        {
            public readonly IPathieModifier Modifier;
            public readonly LodMask LodMask;

            public ChildModifier(IPathieModifier modifier, LodMask lodMask)
            {
                Modifier = modifier;
                LodMask = lodMask;
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

            public ModifiedPathieFactoryBuilder Concat(IPathieModifier modifier, LodMask lod = LodMask.All)
            {
                _children.Add(new ChildModifier(modifier, lod));
                return this;
            }

            public ModifiedPathieFactory ToFactory() => new ModifiedPathieFactory(_factory, _children);
        }
    }
}