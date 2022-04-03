using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshBuilder.Models.Extensions;
using Silksprite.MeshBuilder.Models.Meshes.Modifiers;

namespace Silksprite.MeshBuilder.Models.Meshes.Core
{
    public class ModifiedMeshieFactory : IMeshieFactory
    {
        readonly IMeshieFactory _factory;
        readonly ChildModifier[] _modifiers;

        ModifiedMeshieFactory(IMeshieFactory factory, IEnumerable<ChildModifier> modifiers)
        {
            _factory = factory;
            _modifiers = modifiers.ToArray();
        }

        public Meshie Build(LodMaskLayer lod)
        {
            var result = _factory.Build(lod);
            return _modifiers.Where(modifier => modifier.LodMask.HasLayer(lod))
                .Aggregate(result, (current, modifier) => modifier.Modifier.Modify(current));
        }

        public static ModifiedMeshieFactoryBuilder Builder(IMeshieFactory factory) => new ModifiedMeshieFactoryBuilder(factory);

        class ChildModifier
        {
            public readonly IMeshieModifier Modifier;
            public readonly LodMask LodMask;

            public ChildModifier(IMeshieModifier modifier, LodMask lodMask)
            {
                Modifier = modifier;
                LodMask = lodMask;
            }
        }

        public class ModifiedMeshieFactoryBuilder
        {
            readonly IMeshieFactory _factory;
            readonly List<ChildModifier> _modifiers = new List<ChildModifier>();

            public ModifiedMeshieFactoryBuilder(IMeshieFactory factory) => _factory = factory;

            public ModifiedMeshieFactoryBuilder Concat(IMeshieModifier modifier, LodMask lod = LodMask.All)
            {
                _modifiers.Add(new ChildModifier(modifier, lod));
                return this;
            }

            public ModifiedMeshieFactory ToFactory() => new ModifiedMeshieFactory(_factory, _modifiers);
        }
    }
}