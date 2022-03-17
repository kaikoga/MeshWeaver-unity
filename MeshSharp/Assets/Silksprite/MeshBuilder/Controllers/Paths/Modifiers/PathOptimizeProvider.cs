using Silksprite.MeshBuilder.Controllers.Base.Modifiers;
using Silksprite.MeshBuilder.Models.Base;
using Silksprite.MeshBuilder.Models.Paths.Modifiers;

namespace Silksprite.MeshBuilder.Controllers.Paths.Modifiers
{
    public class PathOptimizeProvider : PathModifierProviderBase
    {
        public override IPathieModifier PathieModifier => new PathOptimize();
    }
}