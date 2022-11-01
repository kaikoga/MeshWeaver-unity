using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshWeaver.Models.Extensions;
using Silksprite.MeshWeaver.Models.Paths.Modifiers;

namespace Silksprite.MeshWeaver.Models.Paths.Core
{
    public class ModifiedPathieFactory : IPathieFactory
    {
        readonly IPathieFactory _factory;
        readonly LodMask _lodMask;
        readonly ChildModifier[] _modifiers;

        ModifiedPathieFactory(IPathieFactory factory, LodMask lodMask, IEnumerable<ChildModifier> modifiers)
        {
            _factory = factory;
            _lodMask = lodMask;
            _modifiers = modifiers.ToArray();
        }

        public Pathie Build(LodMaskLayer lod)
        {
            if (!_lodMask.HasLayer(lod)) return Pathie.Empty();

            var result = _factory.Build(lod);

            if (_modifiers.Length == 0) return result;
            return _modifiers.Where(modifier => modifier.LodMask.HasLayer(lod))
                .Aggregate(result, (current, modifier) => modifier.Modifier.Modify(current));
        }
            
        public static ModifiedPathieFactoryBuilder Builder(IPathieFactory factory, LodMask lodMask) => new ModifiedPathieFactoryBuilder(factory, lodMask);

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
            readonly LodMask _lodMask;
            readonly List<ChildModifier> _children = new List<ChildModifier>();

            public ModifiedPathieFactoryBuilder(IPathieFactory factory, LodMask lodMask)
            {
                _factory = factory;
                _lodMask = lodMask;
            }

            public ModifiedPathieFactoryBuilder Concat(IPathieModifier modifier, LodMask lodMask = LodMask.All)
            {
                _children.Add(new ChildModifier(modifier, lodMask));
                return this;
            }

            public ModifiedPathieFactory ToFactory() => new ModifiedPathieFactory(_factory, _lodMask, _children);
        }
    }
}