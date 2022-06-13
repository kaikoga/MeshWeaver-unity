using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.CustomDrawers;
using Silksprite.MeshWeaver.Models.Modifiers;
using Silksprite.MeshWeaver.Models.Modifiers.Base;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Modifiers
{
    public class UvRemapperProvider : VertwiseModifierProviderBase
    {
        public Rect uvArea = new Rect(0f, 0f, 1f, 1f);

        protected override VertwiseModifierBase VertwiseModifier => new UvRemapper(uvArea);
    }
}