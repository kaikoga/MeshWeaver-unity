using System.Linq;
using Silksprite.MeshBuilder.Controllers.Base.Modifiers;
using Silksprite.MeshBuilder.Extensions;
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
            var meshie = CreateFactory(lod).Build(lod);
            var lastMeshie = GetComponents<IMeshModifierProvider>()
                .Where(provider => provider.enabled && provider.LodMask.HasLayer(lod))
                .Select(provider => provider.MeshieModifier)
                .Aggregate(meshie, (current, modifier) => modifier.Modify(current));
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
            return CreateFactory(lod);
        }

        protected abstract IMeshieFactory CreateFactory(LodMaskLayer lod);
    }

}