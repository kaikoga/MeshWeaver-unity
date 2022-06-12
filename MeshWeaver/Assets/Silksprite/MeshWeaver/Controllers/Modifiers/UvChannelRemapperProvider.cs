using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Models.Modifiers;
using Silksprite.MeshWeaver.Models.Modifiers.Base;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Modifiers
{
    public class UvChannelRemapperProvider : VertwiseModifierProviderBase
    {
        public Rect uvArea = new Rect(0f, 0f, 1f, 1f);
        public int uvChannel;

        protected override VertwiseModifierBase VertwiseModifier => new UvChannelRemapper(uvArea, uvChannel);
    }
}