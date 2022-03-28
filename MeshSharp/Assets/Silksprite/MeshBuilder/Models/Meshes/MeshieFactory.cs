using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshBuilder.Models.Base;

namespace Silksprite.MeshBuilder.Models.Meshes
{
    public static class MeshieFactory
    {
        public static readonly IMeshieFactory Empty = new EmptyMeshieFactory();

        class EmptyMeshieFactory : IMeshieFactory
        {
            public Meshie Build(LodMaskLayer lod) => Meshie.Empty();
        }
    }

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
            return _modifiers.Aggregate(_factory.Build(lod), (current, modifier) => modifier.Modifier.Modify(current));
        }

        public static ModifiedMeshieFactoryBuilder Builder(IMeshieFactory factory) => new ModifiedMeshieFactoryBuilder(factory);

        class ChildModifier
        {
            public readonly IMeshieModifier Modifier;

            public ChildModifier(IMeshieModifier modifier)
            {
                Modifier = modifier;
            }
        }

        public class ModifiedMeshieFactoryBuilder
        {
            readonly IMeshieFactory _factory;
            readonly List<ChildModifier> _modifiers = new List<ChildModifier>();

            public ModifiedMeshieFactoryBuilder(IMeshieFactory factory) => _factory = factory;

            public ModifiedMeshieFactoryBuilder Concat(IMeshieModifier modifier)
            {
                _modifiers.Add(new ChildModifier(modifier));
                return this;
            }

            public ModifiedMeshieFactory ToFactory() => new ModifiedMeshieFactory(_factory, _modifiers);
        }
    }
}