using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Context
{
    public sealed class NullMeshContext : IMeshContext
    {
        NullMeshContext() { }

        public static NullMeshContext Instance => new NullMeshContext();

        public int GetMaterialIndex(Material material) => 0;

        public void Dispose() { }
    }
}