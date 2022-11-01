using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Models.Modifiers;
using Silksprite.MeshWeaver.Models.Modifiers.Base;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Modifiers
{
    public class VertwiseQuantizeProvider : VertwiseModifierProviderBase
    {
        [Min(1)]
        public int denominator = 1;

        protected override VertwiseModifierBase CreateModifier() => new VertwiseQuantize(denominator);
    }
}