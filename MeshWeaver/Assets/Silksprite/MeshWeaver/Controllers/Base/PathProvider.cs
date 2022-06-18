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

        protected sealed override IPathieFactory CreateObject(IMeshContext context) => CreateFactory();
        protected abstract IPathieFactory CreateFactory();

    }
}