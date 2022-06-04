using System.Linq;
using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Controllers.Context;
using Silksprite.MeshWeaver.Models;
using Silksprite.MeshWeaver.Models.Meshes;
using Silksprite.MeshWeaver.Models.Meshes.Core;

namespace Silksprite.MeshWeaver.Controllers.Base
{
    public abstract class MeshProvider : GeometryProvider
    {
        public LodMask lodMask = LodMask.All;

        public IMeshieFactory LastFactory { get; private set; }

        public IMeshieFactory ToFactory(IMeshContext context)
        {
            var providers = GetComponents<IMeshModifierProvider>()
                .Where(provider => provider.enabled);
            LastFactory = providers.Aggregate(ModifiedMeshieFactory.Builder(CreateFactory(context)),
                (builder, provider) => builder.Concat(provider.MeshieModifier, provider.LodMask))
                .ToFactory();
            return LastFactory;
        }

        protected abstract IMeshieFactory CreateFactory(IMeshContext context);
    }

}