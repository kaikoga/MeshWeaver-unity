using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.CustomDrawers;
using Silksprite.MeshWeaver.Models.Paths.Modifiers;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Paths.Modifiers
{
    public class UvGeneratorProvider : PathModifierProviderBase
    {
        [RectCustom(true)]
        public Rect uvArea;
        public bool absoluteScale;
        public float topologicalWeight;
        public int uvChannel;

        protected override IPathieModifier CreateModifier() => new UvGenerator(uvArea, absoluteScale, topologicalWeight, uvChannel);
    }
}