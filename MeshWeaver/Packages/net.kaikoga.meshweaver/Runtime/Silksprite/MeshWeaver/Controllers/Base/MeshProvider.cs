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

        protected sealed override IMeshieFactory CreateObject()
        {
            var providers = GetComponents<IMeshModifierProvider>()
                .Where(provider => provider.enabled);
            return providers.Aggregate(ModifiedMeshieFactory.Builder(CreateFactory(), lodMask),
                    (builder, provider) => builder.Concat(provider.MeshieModifier, provider.LodMask))
                .ToFactory();
        }

        protected abstract IMeshieFactory CreateFactory();
    }

}