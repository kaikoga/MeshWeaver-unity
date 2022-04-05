using Silksprite.MeshWeaver.Models.Paths.Modifiers;

namespace Silksprite.MeshWeaver.Controllers.Base.Modifiers
{
    public interface IPathModifierProvider : IModifierProvider
    {
        IPathieModifier PathieModifier { get; }
    }
}