using System.Linq;
using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Controllers.Context;
using Silksprite.MeshWeaver.Models;
using Silksprite.MeshWeaver.Models.Paths;
using Silksprite.MeshWeaver.Models.Paths.Core;

namespace Silksprite.MeshWeaver.Controllers.Base
{
    public abstract partial class PathProvider : GeometryProvider<IPathieFactory>
    {
        public LodMask lodMask = LodMask.All;

        public IPathieFactory LastFactory => CachedObject;

        public IPathieFactory ToFactory(IMeshContext context) => FindOrCreateObject(context);

        protected sealed override IPathieFactory CreateObject(IMeshContext context)
        {
            var providers = GetComponents<IPathModifierProvider>()
                .Where(provider => provider.enabled);
            return providers.Aggregate(ModifiedPathieFactory.Builder(CreateFactory(context), lodMask),
                    (builder, provider) => builder.Concat(provider.PathieModifier, provider.LodMask))
                .ToFactory();
        }

        protected abstract IPathieFactory CreateFactory(IMeshContext context);

    }
}