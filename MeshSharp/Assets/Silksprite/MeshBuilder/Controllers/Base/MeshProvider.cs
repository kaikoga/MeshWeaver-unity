using System.Linq;
using Silksprite.MeshBuilder.Controllers.Base.Modifiers;
using Silksprite.MeshBuilder.Models;
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
            var lastMeshie = ToFactory().Build(lod);
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

        public IMeshieFactory ToFactory()
        {
            var providers = GetComponents<IMeshModifierProvider>()
                .Where(provider => provider.enabled);
            return providers.Aggregate(ModifiedMeshieFactory.Builder(CreateFactory()),
                (builder, provider) => builder.Concat(provider.MeshieModifier, provider.LodMask))
                .ToFactory();
        }

        protected abstract IMeshieFactory CreateFactory();
    }

}