using Silksprite.MeshBuilder.Models.Paths.Modifiers;

namespace Silksprite.MeshBuilder.Controllers.Base.Modifiers
{
    public abstract class PathModifierProviderBase : ModifierProviderBase, IPathModifierProvider
    {
        public abstract IPathieModifier PathieModifier { get; }
    }
}