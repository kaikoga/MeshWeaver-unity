using Silksprite.MeshBuilder.Models.Base;

namespace Silksprite.MeshBuilder.Controllers.Base.Modifiers
{
    public interface IMeshModifierProvider : IModifierProvider
    {
        IMeshieModifier MeshieModifier { get; }
    }
}