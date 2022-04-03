using Silksprite.MeshBuilder.Models.Meshes.Modifiers;

namespace Silksprite.MeshBuilder.Controllers.Base.Modifiers
{
    public interface IMeshModifierProvider : IModifierProvider
    {
        IMeshieModifier MeshieModifier { get; }
    }
}