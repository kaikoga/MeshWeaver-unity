using Silksprite.MeshBuilder.Models.Base;

namespace Silksprite.MeshBuilder.Controllers.Base.Modifiers
{
    public abstract class MeshModifierProvider : ModifierProvider, IMeshModifierProvider
    {
        public abstract IMeshieModifier MeshieModifier { get; }
    }

    public interface IMeshModifierProvider : IModifierProvider
    {
        IMeshieModifier MeshieModifier { get; }
    }

}