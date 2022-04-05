using Silksprite.MeshWeaver.Models.Meshes.Modifiers;

namespace Silksprite.MeshWeaver.Controllers.Base.Modifiers
{
    public abstract class MeshModifierProviderBase : ModifierProviderBase, IMeshModifierProvider
    {
        public abstract IMeshieModifier MeshieModifier { get; }
    }
}