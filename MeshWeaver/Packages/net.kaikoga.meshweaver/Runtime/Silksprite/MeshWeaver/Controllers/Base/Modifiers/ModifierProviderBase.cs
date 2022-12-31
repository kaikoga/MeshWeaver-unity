using Silksprite.MeshWeaver.Controllers.Context;
using Silksprite.MeshWeaver.Models;

namespace Silksprite.MeshWeaver.Controllers.Base.Modifiers
{
    public abstract class ModifierProviderBase<T> : SubProviderBase<T>
    where T : class
    {
        public LodMask lodMask = LodMask.All;
        
        public LodMask LodMask
        {
            get => lodMask;
            set => lodMask = value;
        }

        protected sealed override T CreateObject(IMeshContext context) => CreateModifier();

        protected abstract T CreateModifier();
    }
}