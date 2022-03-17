using Silksprite.MeshBuilder.Controllers.Base.Modifiers;
using Silksprite.MeshBuilder.Models.Base;
using Silksprite.MeshBuilder.Models.Modifiers;

namespace Silksprite.MeshBuilder.Controllers.Modifiers
{
    public class VertwiseNormalizeProvider : VertwiseModifierProviderBase
    {
        protected override VertwiseModifierBase VertwiseModifier => new MeshNormalize();
    }
}