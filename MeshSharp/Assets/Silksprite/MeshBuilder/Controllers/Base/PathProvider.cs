using System.Linq;
using Silksprite.MeshBuilder.Controllers.Base.Modifiers;
using Silksprite.MeshBuilder.Models;
using Silksprite.MeshBuilder.Models.Paths;
using Silksprite.MeshBuilder.Models.Paths.Core;
using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers.Base
{
    public abstract class PathProvider : GeometryProvider
    {
        public LodMask lodMask = LodMask.All;

        public IPathieFactory LastFactory { get; private set; }

        public IPathieFactory ToFactory()
        {
            var providers = GetComponents<IPathModifierProvider>()
                .Where(provider => provider.enabled);
            LastFactory = providers.Aggregate(ModifiedPathieFactory.Builder(CreateFactory()),
                (builder, provider) => builder.Concat(provider.PathieModifier, provider.LodMask))
                .ToFactory();
            return LastFactory;
        }

        protected abstract IPathieFactory CreateFactory();

        void OnDrawGizmosSelected()
        {
            Gizmos.DrawIcon(transform.position, "curvekeyframeselected", false); // it works!
        }
    }
}