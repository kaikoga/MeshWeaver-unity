using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshWeaver.Models.Extensions;
using Silksprite.MeshWeaver.Models.Meshes.Modifiers;

namespace Silksprite.MeshWeaver.Models.Meshes.Core
{
    public class ModifiedMeshieFactory : IMeshieFactory
    {
        readonly IMeshieFactory _factory;
        readonly LodMask _lodMask;
        readonly ChildModifier[] _modifiers;

        ModifiedMeshieFactory(IMeshieFactory factory, LodMask lodMask, IEnumerable<ChildModifier> modifiers)
        {
            _factory = factory;
            _lodMask = lodMask;
            _modifiers = modifiers.ToArray();
        }

        public Meshie Build(LodMaskLayer lod)
        {
            if (!_lodMask.HasLayer(lod)) return Meshie.Empty();

            var result = _factory.Build(lod);

            if (_modifiers.Length == 0) return result;
            return _modifiers.Where(modifier => modifier.LodMask.HasLayer(lod))
                .Aggregate(result, (current, modifier) => modifier.Modifier.Modify(current));
        }

        public Pathie Extract(string pathName, LodMaskLayer lod) => _factory.Extract(pathName, lod);

        public static ModifiedMeshieFactoryBuilder Builder(IMeshieFactory factory, LodMask lodMask) => new ModifiedMeshieFactoryBuilder(factory, lodMask);

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
            readonly LodMask _lodMask;
            readonly List<ChildModifier> _modifiers = new List<ChildModifier>();

            public ModifiedMeshieFactoryBuilder(IMeshieFactory factory, LodMask lodMask)
            {
                _factory = factory;
                _lodMask = lodMask;
            }

            public ModifiedMeshieFactoryBuilder Concat(IMeshieModifier modifier, LodMask lodMask = LodMask.All)
            {
                _modifiers.Add(new ChildModifier(modifier, lodMask));
                return this;
            }

            public ModifiedMeshieFactory ToFactory() => new ModifiedMeshieFactory(_factory, _lodMask, _modifiers);
        }
    }
}