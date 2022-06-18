using Silksprite.MeshWeaver.Controllers.Context;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Base
{
    [DisallowMultipleComponent]
    public abstract class GeometryProvider<T> : ProviderBase<T>
    where T : class
    {
    }
}