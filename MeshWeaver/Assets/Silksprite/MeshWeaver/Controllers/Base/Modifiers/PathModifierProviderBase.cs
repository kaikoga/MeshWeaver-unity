using Silksprite.MeshWeaver.Models.Paths.Modifiers;

namespace Silksprite.MeshWeaver.Controllers.Base.Modifiers
{
    public abstract class PathModifierProviderBase : ModifierProviderBase<IPathieModifier>, IPathModifierProvider
    {
        public IPathieModifier PathieModifier => CachedModifier;
    }
}