using System.Linq;
using Silksprite.MeshBuilder.Controllers.Base.Modifiers;
using Silksprite.MeshBuilder.Models;
using Silksprite.MeshBuilder.Models.Extensions;
using Silksprite.MeshBuilder.Models.Paths;
using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers.Base
{
    public abstract class PathProvider : GeometryProvider
    {
        public LodMask lodMask = LodMask.All;

        public Pathie LastPathie { get; private set; }
        public Pathie LastColliderPathie { get; private set; }

        public Pathie ToPathie(LodMaskLayer lod)
        {
            var lastPathie = ToFactory(lod).Build(lod);
            if (lod == LodMaskLayer.Collider)
            {
                LastColliderPathie = lastPathie;
            }
            else
            {
                LastPathie = lastPathie;
            }
            return lastPathie;
        }

        public IPathieFactory ToFactory(LodMaskLayer lod)
        {
            var providers = GetComponents<IPathModifierProvider>()
                .Where(provider => provider.enabled && provider.LodMask.HasLayer(lod));
            return providers.Aggregate(ModifiedPathieFactory.Builder(CreateFactory(lod)),
                (builder, provider) => builder.Concat(provider.PathieModifier))
                .ToFactory();
        }

        protected abstract IPathieFactory CreateFactory(LodMaskLayer lod);

        void OnDrawGizmosSelected()
        {
            Gizmos.DrawIcon(transform.position, "curvekeyframeselected", false); // it works!
        }
    }
}