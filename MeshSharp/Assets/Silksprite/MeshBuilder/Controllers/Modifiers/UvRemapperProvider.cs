using Silksprite.MeshBuilder.Controllers.Base.Modifiers;
using Silksprite.MeshBuilder.Models.Base;
using Silksprite.MeshBuilder.Models.Modifiers;
using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers.Modifiers
{
    public class UvRemapperProvider : VertwiseModifierProviderBase
    {
        public Vector2 min = Vector2.zero;
        public Vector2 max = Vector2.one;

        protected override VertwiseModifierBase VertwiseModifier => new UvRemapper(min, max);
    }
}