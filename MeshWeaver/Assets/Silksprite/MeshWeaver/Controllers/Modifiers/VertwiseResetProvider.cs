using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Models.Modifiers;
using Silksprite.MeshWeaver.Models.Modifiers.Base;

namespace Silksprite.MeshWeaver.Controllers.Modifiers
{
    public class VertwiseResetProvider : VertwiseModifierProviderBase
    {
        public bool resetPosition = false;
        public bool resetRotation = true;
        public bool resetScale = true;

        protected override VertwiseModifierBase VertwiseModifier => new VertwiseReset(resetPosition, resetRotation, resetScale);
    }
}