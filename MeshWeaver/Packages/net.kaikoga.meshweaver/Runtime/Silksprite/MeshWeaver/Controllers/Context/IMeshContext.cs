using System;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Context
{
    [Obsolete]
    public interface IMeshContext : IDisposable
    {
        int GetMaterialIndex(Material material);
    }
}