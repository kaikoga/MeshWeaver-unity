using Silksprite.MeshBuilder.Models.Meshes.Modifiers;

namespace Silksprite.MeshBuilder.Controllers.Base.Modifiers
{
    public abstract class MeshModifierProviderBase : ModifierProviderBase, IMeshModifierProvider
    {
        public abstract IMeshieModifier MeshieModifier { get; }
    }
}