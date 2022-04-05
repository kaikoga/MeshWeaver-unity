using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Models.Paths.Modifiers;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Paths.Modifiers
{
    public class PathSubdivisionProvider : PathModifierProviderBase
    {
        public int count = 2;

        public override IPathieModifier PathieModifier => new PathieSubdivision(count);
    }
}