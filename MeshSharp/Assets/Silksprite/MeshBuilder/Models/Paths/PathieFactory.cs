using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshBuilder.Models.Base;

namespace Silksprite.MeshBuilder.Models.Paths
{
    public static class PathieFactory
    {
        public static readonly IPathieFactory Empty = new EmptyPathieFactory();

        public static IPathieFactory WithModifiers(this IPathieFactory factory, IEnumerable<IPathieModifier> modifiers)
        {
            return new ModifiedPathieFactory(factory, modifiers);    
        }

        class EmptyPathieFactory : IPathieFactory
        {
            public Pathie Build(LodMaskLayer lod) => Pathie.Empty();
        }

        class ModifiedPathieFactory : IPathieFactory
        {
            readonly IPathieFactory _factory;
            readonly IPathieModifier[] _modifiers;

            public ModifiedPathieFactory(IPathieFactory factory, IEnumerable<IPathieModifier> modifiers)
            {
                _factory = factory;
                _modifiers = modifiers.ToArray();
            }

            public Pathie Build(LodMaskLayer lod)
            {
                return _modifiers.Aggregate(_factory.Build(lod), (current, modifier) => modifier.Modify(current));
            }
        }
    }
}