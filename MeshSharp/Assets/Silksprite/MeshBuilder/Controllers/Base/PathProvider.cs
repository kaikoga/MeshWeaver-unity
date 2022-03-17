using System.Linq;
using Silksprite.MeshBuilder.Controllers.Base.Modifiers;
using Silksprite.MeshBuilder.Extensions;
using Silksprite.MeshBuilder.Models;
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
            var pathie = GeneratePathie(lod);
            var lastPathie = GetComponents<IPathModifierProvider>()
                .Where(provider => provider.enabled && provider.LodMask.HasLayer(lod))
                .Select(provider => provider.PathieModifier)
                .Aggregate(pathie, (current, modifier) => modifier.Modify(current));
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

        protected abstract Pathie GeneratePathie(LodMaskLayer lod);

        void OnDrawGizmosSelected()
        {
            Gizmos.DrawIcon(transform.position, "curvekeyframeselected", false); // it works!
        }
    }
}