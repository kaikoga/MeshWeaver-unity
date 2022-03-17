using Silksprite.MeshBuilder.Models.Base;

namespace Silksprite.MeshBuilder.Controllers.Base.Modifiers
{
    public abstract class PathModifierProvider : ModifierProvider, IPathModifierProvider
    {
        public abstract IPathieModifier PathieModifier { get; }
    }

    public interface IPathModifierProvider : IModifierProvider
    {
        IPathieModifier PathieModifier { get; }
    }

}