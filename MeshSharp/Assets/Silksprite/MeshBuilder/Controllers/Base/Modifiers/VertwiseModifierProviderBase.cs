using Silksprite.MeshBuilder.Models.Meshes.Modifiers;
using Silksprite.MeshBuilder.Models.Modifiers.Base;
using Silksprite.MeshBuilder.Models.Paths.Modifiers;

namespace Silksprite.MeshBuilder.Controllers.Base.Modifiers
{
    public abstract class VertwiseModifierProviderBase : ModifierProviderBase, IMeshModifierProvider, IPathModifierProvider
    {
        public IMeshieModifier MeshieModifier => VertwiseModifier;
        public IPathieModifier PathieModifier => VertwiseModifier;

        protected abstract VertwiseModifierBase VertwiseModifier { get; }
    }
}