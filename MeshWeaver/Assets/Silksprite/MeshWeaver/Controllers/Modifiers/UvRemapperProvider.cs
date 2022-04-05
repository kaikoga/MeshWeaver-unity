using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Models.Modifiers;
using Silksprite.MeshWeaver.Models.Modifiers.Base;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Modifiers
{
    public class UvRemapperProvider : VertwiseModifierProviderBase
    {
        public Vector2 min = Vector2.zero;
        public Vector2 max = Vector2.one;

        protected override VertwiseModifierBase VertwiseModifier => new UvRemapper(min, max);
    }
}