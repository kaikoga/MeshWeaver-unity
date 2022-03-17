using Silksprite.MeshBuilder.Controllers.Base.Modifiers;
using Silksprite.MeshBuilder.Models.Base;
using Silksprite.MeshBuilder.Models.Paths.Modifiers;
using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers.Paths.Modifiers
{
    public class UvGeneratorProvider : PathModifierProvider
    {
        public Vector2 min;
        public Vector2 max;
        public int minIndex;

        public override IPathieModifier PathieModifier => new UvGenerator(min, max, minIndex);
    }
}