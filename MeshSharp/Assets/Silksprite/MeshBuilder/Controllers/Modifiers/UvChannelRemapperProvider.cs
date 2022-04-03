using Silksprite.MeshBuilder.Controllers.Base.Modifiers;
using Silksprite.MeshBuilder.Models.Modifiers;
using Silksprite.MeshBuilder.Models.Modifiers.Base;
using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers.Modifiers
{
    public class UvChannelRemapperProvider : VertwiseModifierProviderBase
    {
        public Vector2 min = Vector2.zero;
        public Vector2 max = Vector2.one;
        public int uvChannel;

        protected override VertwiseModifierBase VertwiseModifier => new UvChannelRemapper(min, max, uvChannel);
    }
}