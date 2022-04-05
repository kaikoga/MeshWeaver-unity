using Silksprite.MeshWeaver.Models.Meshes.Modifiers;

namespace Silksprite.MeshWeaver.Controllers.Base.Modifiers
{
    public interface IMeshModifierProvider : IModifierProvider
    {
        IMeshieModifier MeshieModifier { get; }
    }
}