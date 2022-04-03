using Silksprite.MeshBuilder.Controllers.Base.Modifiers;
using Silksprite.MeshBuilder.Models.Modifiers;
using Silksprite.MeshBuilder.Models.Modifiers.Base;
using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers.Modifiers
{
    public class VertwiseNormalizeProvider : VertwiseModifierProviderBase
    {
        public Vector3 min = Vector3.zero;
        public Vector3 max = Vector3.one;

        protected override VertwiseModifierBase VertwiseModifier => new VertwiseNormalize(min, max);
    }
}