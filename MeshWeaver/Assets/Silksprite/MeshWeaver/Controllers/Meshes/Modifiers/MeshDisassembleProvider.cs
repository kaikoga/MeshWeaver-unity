using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Models.Meshes.Modifiers;

namespace Silksprite.MeshWeaver.Controllers.Meshes.Modifiers
{
    public class MeshDisassembleProvider : MeshModifierProviderBase
    {
        protected override IMeshieModifier CreateModifier() => new MeshDisassemble();
    }
}