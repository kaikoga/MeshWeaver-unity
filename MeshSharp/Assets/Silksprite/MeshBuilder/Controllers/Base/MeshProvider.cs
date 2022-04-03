using System.Linq;
using Silksprite.MeshBuilder.Controllers.Base.Modifiers;
using Silksprite.MeshBuilder.Models;
using Silksprite.MeshBuilder.Models.Meshes;
using Silksprite.MeshBuilder.Models.Meshes.Core;

namespace Silksprite.MeshBuilder.Controllers.Base
{
    public abstract class MeshProvider : GeometryProvider
    {
        public LodMask lodMask = LodMask.All;

        public IMeshieFactory LastFactory { get; private set; }

        public IMeshieFactory ToFactory()
        {
            var providers = GetComponents<IMeshModifierProvider>()
                .Where(provider => provider.enabled);
            LastFactory = providers.Aggregate(ModifiedMeshieFactory.Builder(CreateFactory()),
                (builder, provider) => builder.Concat(provider.MeshieModifier, provider.LodMask))
                .ToFactory();
            return LastFactory;
        }

        protected abstract IMeshieFactory CreateFactory();
    }

}