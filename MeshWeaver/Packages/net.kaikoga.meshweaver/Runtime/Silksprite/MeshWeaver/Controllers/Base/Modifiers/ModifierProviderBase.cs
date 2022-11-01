using Silksprite.MeshWeaver.Controllers.Context;
using Silksprite.MeshWeaver.Models;

namespace Silksprite.MeshWeaver.Controllers.Base.Modifiers
{
    public abstract class ModifierProviderBase<T> : ProviderBase<T>
    where T : class
    {
        public LodMask lodMask = LodMask.All;
        
        public LodMask LodMask
        {
            get => lodMask;
            set => lodMask = value;
        }

        // ReSharper disable once Unity.RedundantEventFunction
        void Start()
        {
            // The sole reason for this empty method is for showing enabled checkbox
        }
        
        protected sealed override T CreateObject(IMeshContext context) => CreateModifier();

        protected abstract T CreateModifier();
    }
}