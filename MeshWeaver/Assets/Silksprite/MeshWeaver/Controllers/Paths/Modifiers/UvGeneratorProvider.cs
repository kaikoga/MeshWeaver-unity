using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Models.Paths.Modifiers;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Paths.Modifiers
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