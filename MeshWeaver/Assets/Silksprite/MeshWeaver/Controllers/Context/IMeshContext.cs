using System;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Context
{
    public interface IMeshContext : IDisposable
    {
        int GetMaterialIndex(Material material);
    }
}