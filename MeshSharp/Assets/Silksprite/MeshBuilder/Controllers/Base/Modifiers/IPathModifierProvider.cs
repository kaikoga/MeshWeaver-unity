using Silksprite.MeshBuilder.Models.Paths.Modifiers;

namespace Silksprite.MeshBuilder.Controllers.Base.Modifiers
{
    public interface IPathModifierProvider : IModifierProvider
    {
        IPathieModifier PathieModifier { get; }
    }
}