using Silksprite.MeshBuilder.Models.Base;

namespace Silksprite.MeshBuilder.Controllers.Base.Modifiers
{
    public abstract class VertwiseModifierProviderBase : ModifierProviderBase, IMeshModifierProvider, IPathModifierProvider
    {
        public IMeshieModifier MeshieModifier => VertwiseModifier;
        public IPathieModifier PathieModifier => VertwiseModifier;

        protected abstract VertwiseModifierBase VertwiseModifier { get; }
    }
}