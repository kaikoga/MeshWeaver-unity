using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Models.Modifiers;
using Silksprite.MeshWeaver.Models.Modifiers.Base;

namespace Silksprite.MeshWeaver.Controllers.Modifiers
{
    public class VertwiseAlignProvider : VertwiseModifierProviderBase
    {
        public bool alignPosition = false;
        public bool alignRotation = true;
        public bool alignScale = true;

        protected override VertwiseModifierBase VertwiseModifier => new VertwiseAlign(alignPosition, alignRotation, alignScale);
    }
}