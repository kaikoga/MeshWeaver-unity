using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Models.Paths.Modifiers;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Paths.Modifiers
{
    public class UvGeneratorProvider : PathModifierProviderBase
    {
        public Rect uvArea;
        public bool absoluteScale;
        public float topologicalWeight;
        public int uvChannel;

        public override IPathieModifier PathieModifier => new UvGenerator(uvArea, absoluteScale, topologicalWeight, uvChannel);
    }
}