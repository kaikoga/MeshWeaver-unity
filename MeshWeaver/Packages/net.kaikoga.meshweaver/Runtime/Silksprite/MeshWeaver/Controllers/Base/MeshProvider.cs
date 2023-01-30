using System.Linq;
using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Models;
using Silksprite.MeshWeaver.Models.Meshes;
using Silksprite.MeshWeaver.Models.Meshes.Core;

namespace Silksprite.MeshWeaver.Controllers.Base
{
    public abstract class MeshProvider : GeometryProvider<IMeshieFactory>
    {
        public LodMask lodMask = LodMask.All;

        public IMeshieFactory LastFactory => CachedObject;

        public IMeshieFactory ToFactory() => FindOrCreateObject();

        protected sealed override int Sync() => SyncReferences() ^ GetComponents<IMeshModifierProvider>().Where(provider => provider.enabled).Aggregate(0, (r, content) => r ^ content.Revision);

        protected sealed override IMeshieFactory CreateObject()
        {
            var providers = GetComponents<IMeshModifierProvider>()
                .Where(provider => provider.enabled);
            return providers.Aggregate(ModifiedMeshieFactory.Builder(CreateFactory(), lodMask),
                    (builder, provider) => builder.Concat(provider.MeshieModifier, provider.LodMask))
                .ToFactory();
        }

        protected virtual int SyncReferences() => 0;

        protected abstract IMeshieFactory CreateFactory();
    }

}