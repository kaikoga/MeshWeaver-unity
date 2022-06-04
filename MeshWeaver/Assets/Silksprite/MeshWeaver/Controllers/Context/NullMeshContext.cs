using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Context
{
    public class NullMeshContext : IMeshContext
    {
        NullMeshContext() { }

        public static NullMeshContext Instance => new NullMeshContext();

        public int GetMaterialIndex(Material material) => 0;
    }
}