using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Models.Modifiers;
using Silksprite.MeshWeaver.Models.Modifiers.Base;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Modifiers
{
    public class VertwiseNormalizeProvider : VertwiseModifierProviderBase
    {
        public Vector3 min = Vector3.zero;
        public Vector3 max = Vector3.one;

        protected override VertwiseModifierBase VertwiseModifier => new VertwiseNormalize(min, max);
    }
}