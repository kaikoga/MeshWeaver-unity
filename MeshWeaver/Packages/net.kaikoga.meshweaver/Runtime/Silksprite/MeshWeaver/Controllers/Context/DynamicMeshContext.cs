using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Context
{
    public sealed class DynamicMeshContext : IMeshContext
    {
        List<Material> _materials;

        public DynamicMeshContext() => _materials = new List<Material>();

        public Material[] ToMaterials() => _materials.ToArray();

        public int GetMaterialIndex(Material material)
        {
            if (!material) return 0;

            var index = _materials.IndexOf(material);
            if (index >= 0) return index;

            _materials.Add(material);
            return _materials.Count - 1;
        }

        public void Dispose() => _materials = null;
    }
}