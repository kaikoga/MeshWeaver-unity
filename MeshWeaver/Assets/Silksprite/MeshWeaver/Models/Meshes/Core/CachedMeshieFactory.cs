using System.Collections.Concurrent;

namespace Silksprite.MeshWeaver.Models.Meshes.Core
{
    public class CachedMeshieFactory : IMeshieFactory
    {
        readonly IMeshieFactory _factory;
        readonly ConcurrentDictionary<LodMaskLayer, Meshie> _cache = new ConcurrentDictionary<LodMaskLayer, Meshie>();

        public CachedMeshieFactory(IMeshieFactory factory) => _factory = factory;

        public Meshie Build(LodMaskLayer lod) => _cache.GetOrAdd(lod, l => _factory.Build(l));
    }
}