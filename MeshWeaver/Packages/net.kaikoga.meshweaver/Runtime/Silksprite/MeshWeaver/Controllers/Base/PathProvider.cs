using System.Linq;
using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Models;
using Silksprite.MeshWeaver.Models.Paths;
using Silksprite.MeshWeaver.Models.Paths.Core;

namespace Silksprite.MeshWeaver.Controllers.Base
{
    public abstract partial class PathProvider : GeometryProvider<IPathieFactory>
    {
        public LodMask lodMask = LodMask.All;

        public IPathieFactory LastFactory => CachedObject;

        public IPathieFactory ToFactory() => FindOrCreateObject();

        protected sealed override int Sync() => SyncReferences() ^ GetComponents<IPathModifierProvider>().Where(provider => provider.enabled).Aggregate(0, (r, content) => r ^ content.Revision);

        protected sealed override IPathieFactory CreateObject()
        {
            var providers = GetComponents<IPathModifierProvider>()
                .Where(provider => provider.enabled);
            return providers.Aggregate(ModifiedPathieFactory.Builder(CreateFactory(), lodMask),
                    (builder, provider) => builder.Concat(provider.PathieModifier, provider.LodMask))
                .ToFactory();
        }

        protected virtual int SyncReferences() => 0;

        protected abstract IPathieFactory CreateFactory();
    }
}