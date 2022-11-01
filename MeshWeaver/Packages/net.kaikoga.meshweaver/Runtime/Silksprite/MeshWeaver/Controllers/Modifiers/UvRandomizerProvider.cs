using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Models.Modifiers;
using Silksprite.MeshWeaver.Models.Modifiers.Base;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Modifiers
{
    public class UvRandomizerProvider : VertwiseModifierProviderBase
    {
        public Vector2 @base = Vector2.zero;
        public Vector2 range = Vector2.one;
        public int seed;

        protected override VertwiseModifierBase CreateModifier() => new UvRandomizer(@base, range, seed);
    }
}