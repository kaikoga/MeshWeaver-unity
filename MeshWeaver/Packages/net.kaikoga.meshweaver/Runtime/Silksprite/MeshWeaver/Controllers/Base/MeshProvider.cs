using System.Linq;
using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Controllers.Core;
using Silksprite.MeshWeaver.Models;
using Silksprite.MeshWeaver.Models.Meshes;
using Silksprite.MeshWeaver.Models.Meshes.Core;

namespace Silksprite.MeshWeaver.Controllers.Base
{
    public abstract class MeshProvider : GeometryProvider<IMeshieFactory>
    {
        public LodMask lodMask = LodMask.All;

        readonly ModifierCollector<IMeshModifierProvider> _modifierCollector = new ModifierCollector<IMeshModifierProvider>();

        public IMeshieFactory LastFactory => CachedObject;

        public IMeshieFactory ToFactory() => FindOrCreateObject();

        protected sealed override int Sync() => SyncReferences() ^ _modifierCollector.Sync(GetComponents<IMeshModifierProvider>().Where(provider => provider.enabled)); 

        protected sealed override IMeshieFactory CreateObject()
        {
            return _modifierCollector.Value.Aggregate(ModifiedMeshieFactory.Builder(CreateFactory(), lodMask),
                    (builder, provider) => builder.Concat(provider.MeshieModifier, provider.LodMask))
                .ToFactory();
        }

        protected virtual int SyncReferences() => 0;

        protected abstract IMeshieFactory CreateFactory();
    }

}