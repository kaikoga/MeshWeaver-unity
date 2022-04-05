using Silksprite.MeshWeaver.Models.Meshes.Modifiers;
using Silksprite.MeshWeaver.Models.Modifiers.Base;
using Silksprite.MeshWeaver.Models.Paths.Modifiers;

namespace Silksprite.MeshWeaver.Controllers.Base.Modifiers
{
    public abstract class VertwiseModifierProviderBase : ModifierProviderBase, IMeshModifierProvider, IPathModifierProvider
    {
        public IMeshieModifier MeshieModifier => VertwiseModifier;
        public IPathieModifier PathieModifier => VertwiseModifier;

        protected abstract VertwiseModifierBase VertwiseModifier { get; }
    }
}