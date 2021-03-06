using System.Linq;
using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Controllers.Context;
using Silksprite.MeshWeaver.Models;
using Silksprite.MeshWeaver.Models.Meshes;
using Silksprite.MeshWeaver.Models.Meshes.Core;

namespace Silksprite.MeshWeaver.Controllers.Base
{
    public abstract class MeshProvider : GeometryProvider<IMeshieFactory>
    {
        public LodMask lodMask = LodMask.All;

        public IMeshieFactory LastFactory => CachedObject;

        public IMeshieFactory ToFactory(IMeshContext context) => FindOrCreateObject(context);

        protected sealed override IMeshieFactory CreateObject(IMeshContext context)
        {
            var providers = GetComponents<IMeshModifierProvider>()
                .Where(provider => provider.enabled);
            return providers.Aggregate(ModifiedMeshieFactory.Builder(CreateFactory(context)),
                    (builder, provider) => builder.Concat(provider.MeshieModifier, provider.LodMask))
                .ToFactory();
        }

        protected abstract IMeshieFactory CreateFactory(IMeshContext context);
    }

}