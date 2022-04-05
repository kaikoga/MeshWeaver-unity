using Silksprite.MeshWeaver.Models.Paths.Modifiers;

namespace Silksprite.MeshWeaver.Controllers.Base.Modifiers
{
    public abstract class PathModifierProviderBase : ModifierProviderBase, IPathModifierProvider
    {
        public abstract IPathieModifier PathieModifier { get; }
    }
}