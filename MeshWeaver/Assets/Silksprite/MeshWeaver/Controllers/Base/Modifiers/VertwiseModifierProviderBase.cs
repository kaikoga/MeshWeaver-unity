using Silksprite.MeshWeaver.Models.Meshes.Modifiers;
using Silksprite.MeshWeaver.Models.Modifiers.Base;
using Silksprite.MeshWeaver.Models.Paths.Modifiers;

namespace Silksprite.MeshWeaver.Controllers.Base.Modifiers
{
    public abstract class VertwiseModifierProviderBase : ModifierProviderBase, IMeshModifierProvider, IPathModifierProvider
    {
        public IMeshieModifier MeshieModifier => VertwiseModifier;
        public IPathieModifier PathieModifier => VertwiseModifier;

        VertwiseModifierBase _vertwiseModifier;
        VertwiseModifierBase VertwiseModifier
        {
            get
            {
                if (IsDirty()) _vertwiseModifier = null;
                return _vertwiseModifier ?? (_vertwiseModifier = CreateModifier());
            }
        }

        protected abstract VertwiseModifierBase CreateModifier();
    }
}