using Silksprite.MeshBuilder.Controllers.Base.Modifiers;
using Silksprite.MeshBuilder.Models.Base;
using Silksprite.MeshBuilder.Models.Paths.Modifiers;
using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers.Paths.Modifiers
{
    public class PathRepeatProvider : PathModifierProvider
    {
        public int count = 2;
        public bool aggregate;

        public override PathieModifier Modifier => new PathieRepeat(count, aggregate);
    }
}