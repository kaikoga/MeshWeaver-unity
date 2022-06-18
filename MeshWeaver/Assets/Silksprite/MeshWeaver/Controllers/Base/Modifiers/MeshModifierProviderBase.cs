using Silksprite.MeshWeaver.Models.Meshes.Modifiers;

namespace Silksprite.MeshWeaver.Controllers.Base.Modifiers
{
    public abstract class MeshModifierProviderBase : ModifierProviderBase, IMeshModifierProvider
    {
        IMeshieModifier _meshieModifier;
        public IMeshieModifier MeshieModifier
        {
            get
            {
                if (IsDirty()) _meshieModifier = null;
                return _meshieModifier ?? (_meshieModifier = CreateModifier());
            }
        }
        
        protected abstract IMeshieModifier CreateModifier();
    }
}