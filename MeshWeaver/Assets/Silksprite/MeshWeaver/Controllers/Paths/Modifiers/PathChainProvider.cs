using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Models.Paths.Modifiers;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Paths.Modifiers
{
    public class PathChainProvider : PathModifierProviderBase
    {
        public float rolling = 0f;

        public override IPathieModifier PathieModifier => new PathChain(rolling);
    }
}