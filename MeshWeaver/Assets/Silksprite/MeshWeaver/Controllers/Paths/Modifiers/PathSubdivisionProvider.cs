using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Models.Paths.Modifiers;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Paths.Modifiers
{
    public class PathSubdivisionProvider : PathModifierProviderBase
    {
        [Min(1)]
        public int maxCount = 2;
        [Min(0f)]
        public float maxLength = 1f;

        public override IPathieModifier PathieModifier => new PathSubdivision(maxCount, maxLength);
    }
}