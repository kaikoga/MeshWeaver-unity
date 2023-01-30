using System.Linq;
using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Controllers.Core;
using Silksprite.MeshWeaver.Models;
using Silksprite.MeshWeaver.Models.Paths;
using Silksprite.MeshWeaver.Models.Paths.Core;

namespace Silksprite.MeshWeaver.Controllers.Base
{
    public abstract partial class PathProvider : GeometryProvider<IPathieFactory>
    {
        public LodMask lodMask = LodMask.All;

        readonly ModifierCollector<IPathModifierProvider> _modifierCollector = new ModifierCollector<IPathModifierProvider>();

        public IPathieFactory LastFactory => CachedObject;

        public IPathieFactory ToFactory() => FindOrCreateObject();

        protected sealed override int Sync() => SyncReferences() ^ _modifierCollector.Sync(GetComponents<IPathModifierProvider>().Where(provider => provider.enabled));

        protected sealed override IPathieFactory CreateObject()
        {
            return _modifierCollector.Value.Aggregate(ModifiedPathieFactory.Builder(CreateFactory(), lodMask),
                    (builder, provider) => builder.Concat(provider.PathieModifier, provider.LodMask))
                .ToFactory();
        }

        protected virtual int SyncReferences() => 0;

        protected abstract IPathieFactory CreateFactory();
    }
}