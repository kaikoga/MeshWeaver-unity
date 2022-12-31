namespace Silksprite.MeshWeaver.Controllers.Base
{
    public abstract class SubProviderBase<T> : ProviderBase<T>
    {
        // ReSharper disable once Unity.RedundantEventFunction
        void Start()
        {
            // The sole reason for this empty method is for showing enabled checkbox
        }
    }
}