using System.Linq;
using Silksprite.MeshBuilder.Controllers.Base.Modifiers;
using Silksprite.MeshBuilder.Models;
using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers.Base
{
    public abstract class PathProvider : GeometryProvider
    {
        public LodMask lodMask = LodMask.All;

        public Pathie LastPathie { get; private set; }
        public Pathie LastColliderPathie { get; private set; }

        public Pathie ToPathie(LodMask lod)
        {
            var pathie = GeneratePathie(lod);
            var lastPathie = GetComponents<PathModifierProvider>()
                .Where(provider => provider.enabled && provider.lodMask.HasFlag(lod))
                .Select(provider => provider.Modifier)
                .Aggregate(pathie, (current, modifier) => modifier.Modify(current));
            if (lod == LodMask.Collider)
            {
                LastColliderPathie = lastPathie;
            }
            else
            {
                LastPathie = lastPathie;
            }
            return lastPathie;
        }

        protected abstract Pathie GeneratePathie(LodMask lod);

        void OnDrawGizmosSelected()
        {
            Gizmos.DrawIcon(transform.position, "curvekeyframeselected", false); // it works!
        }
    }
}