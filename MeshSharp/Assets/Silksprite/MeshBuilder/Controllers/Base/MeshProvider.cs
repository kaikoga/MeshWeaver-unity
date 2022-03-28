using System.Linq;
using Silksprite.MeshBuilder.Controllers.Base.Modifiers;
using Silksprite.MeshBuilder.Models;
using Silksprite.MeshBuilder.Models.Extensions;
using Silksprite.MeshBuilder.Models.Meshes;

namespace Silksprite.MeshBuilder.Controllers.Base
{
    public abstract class MeshProvider : GeometryProvider
    {
        public LodMask lodMask = LodMask.All;

        public Meshie LastMeshie { get; private set; }
        public Meshie LastColliderMeshie { get; private set; }

        public Meshie ToMeshie(LodMaskLayer lod)
        {
            var lastMeshie = ToFactory(lod).Build(lod);
            if (lod == LodMaskLayer.Collider)
            {
                LastColliderMeshie = lastMeshie;
            }
            else
            {
                LastMeshie = lastMeshie;
            }
            return lastMeshie;
        }

        public IMeshieFactory ToFactory(LodMaskLayer lod)
        {
            var providers = GetComponents<IMeshModifierProvider>()
                .Where(provider => provider.enabled && provider.LodMask.HasLayer(lod));
            return providers.Aggregate(ModifiedMeshieFactory.Builder(CreateFactory(lod)),
                (builder, provider) => builder.Concat(provider.MeshieModifier, provider.LodMask))
                .ToFactory();
        }

        protected abstract IMeshieFactory CreateFactory(LodMaskLayer lod);
    }

}