using Silksprite.MeshWeaver.Models.Meshes.Modifiers;

namespace Silksprite.MeshWeaver.Controllers.Base.Modifiers
{
    public abstract class MeshModifierProviderBase : ModifierProviderBase<IMeshieModifier>, IMeshModifierProvider
    {
        public IMeshieModifier MeshieModifier => FindOrCreateObject(null);
    }
}