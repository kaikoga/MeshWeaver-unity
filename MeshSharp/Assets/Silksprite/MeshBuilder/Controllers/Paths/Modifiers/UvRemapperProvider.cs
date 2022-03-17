using Silksprite.MeshBuilder.Controllers.Base.Modifiers;
using Silksprite.MeshBuilder.Models.Base;
using Silksprite.MeshBuilder.Models.Paths.Modifiers;
using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers.Paths.Modifiers
{
    public class UvRemapperProvider : PathModifierProvider
    {
        public Vector2 min = Vector2.zero;
        public Vector2 max = Vector2.one;

        public override IPathieModifier PathieModifier => new UvRemapper(min, max);
    }
}