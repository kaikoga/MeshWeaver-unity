using Silksprite.MeshBuilder.Controllers.Base.Modifiers;
using Silksprite.MeshBuilder.Models.Paths.Modifiers;
using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers.Paths.Modifiers
{
    public class PathSubdivisionProvider : PathModifierProviderBase
    {
        public int count = 2;

        public override IPathieModifier PathieModifier => new PathieSubdivision(count);
    }
}