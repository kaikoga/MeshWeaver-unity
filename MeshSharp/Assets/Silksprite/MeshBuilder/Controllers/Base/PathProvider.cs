using System.Linq;
using Silksprite.MeshBuilder.Controllers.Base.Modifiers;
using Silksprite.MeshBuilder.Models;
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
            var lastPathie = ToFactory().Build(lod);
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

        public IPathieFactory ToFactory()
        {
            var providers = GetComponents<IPathModifierProvider>()
                .Where(provider => provider.enabled);
            return providers.Aggregate(ModifiedPathieFactory.Builder(CreateFactory()),
                (builder, provider) => builder.Concat(provider.PathieModifier, provider.LodMask))
                .ToFactory();
        }

        protected abstract IPathieFactory CreateFactory();

        void OnDrawGizmosSelected()
        {
            Gizmos.DrawIcon(transform.position, "curvekeyframeselected", false); // it works!
        }
    }
}