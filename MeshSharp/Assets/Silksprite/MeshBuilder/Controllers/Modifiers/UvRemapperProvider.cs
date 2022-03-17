using Silksprite.MeshBuilder.Controllers.Base.Modifiers;
using Silksprite.MeshBuilder.Models.Base;
using Silksprite.MeshBuilder.Models.Modifiers;
using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers.Modifiers
{
    public class UvRemapperProvider : VertexModifierProviderBase
    {
        public Vector2 min = Vector2.zero;
        public Vector2 max = Vector2.one;

        public override IPathieModifier PathieModifier => new UvRemapper(min, max);
        public override IMeshieModifier MeshieModifier => new UvRemapper(min, max);
    }
}