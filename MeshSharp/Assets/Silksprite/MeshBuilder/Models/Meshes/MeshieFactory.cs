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
            readonly IMeshieModifier[] _modifiers;

            public ModifiedMeshieFactory(IMeshieFactory factory, IEnumerable<IMeshieModifier> modifiers)
            {
                _factory = factory;
                _modifiers = modifiers.ToArray();
            }

            public Meshie Build(LodMaskLayer lod)
            {
                return _modifiers.Aggregate(_factory.Build(lod), (current, modifier) => modifier.Modify(current));
            }
        }
    }
}