using Silksprite.MeshBuilder.Controllers.Base.Modifiers;
using Silksprite.MeshBuilder.Models.Base;
using Silksprite.MeshBuilder.Models.Paths.Modifiers;
using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers.Paths.Modifiers
{
    public class PathSubdivisionProvider : PathModifierProvider
    {
        public int count = 2;

        public override PathieModifier Modifier => new PathieSubdivision(count);
    }
}