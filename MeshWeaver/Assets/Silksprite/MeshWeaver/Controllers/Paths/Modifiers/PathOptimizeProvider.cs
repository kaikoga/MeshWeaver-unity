using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Models.Paths.Modifiers;

namespace Silksprite.MeshWeaver.Controllers.Paths.Modifiers
{
    public class PathOptimizeProvider : PathModifierProviderBase
    {
        protected override IPathieModifier CreateModifier() => new PathOptimize();
    }
}