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

        public Pathie ToPathie(LodMask lod)
        {
            var pathie = GeneratePathie(lod);
            LastPathie = GetComponents<PathModifierProvider>()
                .Where(provider => provider.enabled)
                .Select(provider => provider.Modifier)
                .Aggregate(pathie, (current, modifier) => modifier.Modify(current));
            return LastPathie;
        }

        protected abstract Pathie GeneratePathie(LodMask lod);

        void OnDrawGizmosSelected()
        {
            Gizmos.DrawIcon(transform.position, "curvekeyframeselected", false); // it works!
        }
    }
}