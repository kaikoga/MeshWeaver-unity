using System.Collections.Generic;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Context
{
    public class DynamicMeshContext : IMeshContext
    {
        readonly List<Material> _materials = new List<Material>();

        public Material[] ToMaterials() => _materials.ToArray();

        public int GetMaterialIndex(Material material)
        {
            var index = _materials.IndexOf(material);
            if (index >= 0)
                return index;

            _materials.Add(material);
            return _materials.Count - 1;
        }
    }
}