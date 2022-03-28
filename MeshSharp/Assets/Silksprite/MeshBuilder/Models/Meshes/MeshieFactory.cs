using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshBuilder.Models.Base;

namespace Silksprite.MeshBuilder.Models.Meshes
{
    public static class MeshieFactory
    {
        public static readonly IMeshieFactory Empty = new EmptyMeshieFactory();

        public static IMeshieFactory WithModifiers(this IMeshieFactory factory, IEnumerable<IMeshieModifier> modifiers)
        {
            return new ModifiedMeshieFactory(factory, modifiers);    
        }

        class EmptyMeshieFactory : IMeshieFactory
        {
            public Meshie Build(LodMaskLayer lod) => Meshie.Empty();
        }

        class ModifiedMeshieFactory : IMeshieFactory
        {
            readonly IMeshieFactory _factory;
            readonly ChildModifier[] _modifiers;

            public ModifiedMeshieFactory(IMeshieFactory factory, IEnumerable<IMeshieModifier> modifiers)
            {
                _factory = factory;
                _modifiers = modifiers.Select(modifier => new ChildModifier(modifier)).ToArray();
            }

            public Meshie Build(LodMaskLayer lod)
            {
                return _modifiers.Aggregate(_factory.Build(lod), (current, modifier) => modifier.Modifier.Modify(current));
            }

            class ChildModifier
            {
                public readonly IMeshieModifier Modifier;

                public ChildModifier(IMeshieModifier modifier)
                {
                    Modifier = modifier;
                }
            }
        }
    }
}