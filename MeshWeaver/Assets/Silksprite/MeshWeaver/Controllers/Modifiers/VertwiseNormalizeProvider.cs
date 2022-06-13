using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.CustomDrawers;
using Silksprite.MeshWeaver.Models.Modifiers;
using Silksprite.MeshWeaver.Models.Modifiers.Base;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Modifiers
{
    public class VertwiseNormalizeProvider : VertwiseModifierProviderBase
    {
        [BoundsCustom]
        public Bounds bounds = new Bounds(new Vector3(0.5f, 0.5f, 0.5f), new Vector3(1f, 1f, 1f));

        protected override VertwiseModifierBase VertwiseModifier => new VertwiseNormalize(bounds);
    }
}