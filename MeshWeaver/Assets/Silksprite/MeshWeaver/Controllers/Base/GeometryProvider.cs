using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Base
{
    [DisallowMultipleComponent]
    public abstract class GeometryProvider<T> : ProviderBase<T>
    where T : class
    {
        protected sealed override T CreateObject() => CreateFactory();

        protected abstract T CreateFactory();
    }
}