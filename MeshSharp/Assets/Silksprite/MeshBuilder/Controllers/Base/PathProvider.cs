using System.Linq;
using Silksprite.MeshBuilder.Controllers.Base.Modifiers;
using Silksprite.MeshBuilder.Models;
using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers.Base
{
    public abstract class PathProvider : GeometryProvider
    {
        public Pathie LastPathie { get; private set; }

        public Pathie ToPathie()
        {
            var pathie = GeneratePathie();
            LastPathie = GetComponents<PathModifierProvider>()
                .Where(provider => provider.enabled)
                .Select(provider => provider.Modifier)
                .Aggregate(pathie, (current, modifier) => modifier.Modify(current));
            return LastPathie;
        }

        protected abstract Pathie GeneratePathie();

        void OnDrawGizmosSelected()
        {
            Gizmos.DrawIcon(transform.position, "curvekeyframeselected", false); // it works!
        }
    }
}