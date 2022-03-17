using Silksprite.MeshBuilder.Models.Base;

namespace Silksprite.MeshBuilder.Controllers.Base.Modifiers
{
    public abstract class VertexModifierProviderBase : ModifierProviderBase, IMeshModifierProvider, IPathModifierProvider
    {
        public abstract IMeshieModifier MeshieModifier { get; }
        public abstract IPathieModifier PathieModifier { get; }
    }
}