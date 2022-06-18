using System;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Context
{
    public sealed class StaticMeshContext : IMeshContext
    {
        Material[] _materials;

        public StaticMeshContext(Material[] materials) => _materials = materials;

        public int GetMaterialIndex(Material material)
        {
            var index = Array.IndexOf(_materials, material);
            return index >= 0 ? index : 0;
        }

        public void Dispose() => _materials = null;
    }
}