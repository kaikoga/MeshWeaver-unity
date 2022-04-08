using System.Collections.Concurrent;

namespace Silksprite.MeshWeaver.Models.Paths.Core
{
    public class CachedPathieFactory : IPathieFactory
    {
        readonly IPathieFactory _factory;
        readonly ConcurrentDictionary<LodMaskLayer, Pathie> _cache = new ConcurrentDictionary<LodMaskLayer, Pathie>();

        public CachedPathieFactory(IPathieFactory factory) => _factory = factory;

        public Pathie Build(LodMaskLayer lod) => _cache.GetOrAdd(lod, l => _factory.Build(l));
    }
}