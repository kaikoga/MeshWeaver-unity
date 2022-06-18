using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Models.Paths.Modifiers;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Paths.Modifiers
{
    public class PathChainProvider : PathModifierProviderBase
    {
        public float rolling = 0f;

        protected override IPathieModifier CreateModifier() => new PathChain(rolling);
    }
}