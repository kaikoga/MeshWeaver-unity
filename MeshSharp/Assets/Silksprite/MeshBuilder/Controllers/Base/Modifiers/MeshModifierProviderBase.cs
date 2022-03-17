using Silksprite.MeshBuilder.Models.Base;

namespace Silksprite.MeshBuilder.Controllers.Base.Modifiers
{
    public abstract class MeshModifierProviderBase : ModifierProviderBase, IMeshModifierProvider
    {
        public abstract IMeshieModifier MeshieModifier { get; }
    }
}