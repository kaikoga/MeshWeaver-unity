using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Base
{
    [DisallowMultipleComponent]
    public abstract class GeometryProvider<T> : ProviderBase<T>
    where T : class
    {
        // NOTE: We just can't cache GeometryProviders
        protected override bool RefreshAlways => true;
    }
}