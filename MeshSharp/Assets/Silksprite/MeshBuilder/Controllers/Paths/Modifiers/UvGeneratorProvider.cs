using Silksprite.MeshBuilder.Controllers.Base.Modifiers;
using Silksprite.MeshBuilder.Models.Base;
using Silksprite.MeshBuilder.Models.Paths.Modifiers;
using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers.Paths.Modifiers
{
    public class UvGeneratorProvider : PathModifierProviderBase
    {
        public Vector2 min;
        public Vector2 max;
        public float topologicalWeight;
        public int uvChannel;

        public override IPathieModifier PathieModifier => new UvGenerator(min, max, topologicalWeight, uvChannel);
    }
}