using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Context
{
    public class DynamicMeshContext : IMeshContext
    {
        readonly List<Material> _materials;

        public DynamicMeshContext() => _materials = new List<Material>();
        public DynamicMeshContext(IEnumerable<Material> materials) => _materials = materials.ToList();

        public Material[] ToMaterials() => _materials.ToArray();

        public int GetMaterialIndex(Material material)
        {
            if (!material) return 0;

            var index = _materials.IndexOf(material);
            if (index >= 0) return index;

            _materials.Add(material);
            return _materials.Count - 1;
        }
    }
}