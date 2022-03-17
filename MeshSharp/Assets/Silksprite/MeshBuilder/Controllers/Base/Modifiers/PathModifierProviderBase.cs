using Silksprite.MeshBuilder.Models.Base;

namespace Silksprite.MeshBuilder.Controllers.Base.Modifiers
{
    public abstract class PathModifierProviderBase : ModifierProviderBase, IPathModifierProvider
    {
        public abstract IPathieModifier PathieModifier { get; }
    }
}