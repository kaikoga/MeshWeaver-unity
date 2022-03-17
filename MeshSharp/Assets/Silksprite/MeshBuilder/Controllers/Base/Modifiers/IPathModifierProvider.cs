using Silksprite.MeshBuilder.Models.Base;

namespace Silksprite.MeshBuilder.Controllers.Base.Modifiers
{
    public interface IPathModifierProvider : IModifierProvider
    {
        IPathieModifier PathieModifier { get; }
    }
}