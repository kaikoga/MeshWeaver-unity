using Silksprite.MeshWeaver.Models.Paths.Modifiers;

namespace Silksprite.MeshWeaver.Controllers.Base.Modifiers
{
    public abstract class PathModifierProviderBase : ModifierProviderBase, IPathModifierProvider
    {
        IPathieModifier _pathieModifier;
        public IPathieModifier PathieModifier
        {
            get
            {
                if (IsDirty()) _pathieModifier = null;
                return _pathieModifier ?? (_pathieModifier = CreateModifier());
            }
        }

        protected abstract IPathieModifier CreateModifier();
    }
}