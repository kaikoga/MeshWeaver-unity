using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Models.Paths.Modifiers;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Paths.Modifiers
{
    public class UvGeneratorProvider : PathModifierProviderBase
    {
        public Vector2 min;
        public Vector2 max;
        public bool absoluteScale;
        public float topologicalWeight;
        public int uvChannel;

        public override IPathieModifier PathieModifier => new UvGenerator(min, max, absoluteScale, topologicalWeight, uvChannel);
    }
}