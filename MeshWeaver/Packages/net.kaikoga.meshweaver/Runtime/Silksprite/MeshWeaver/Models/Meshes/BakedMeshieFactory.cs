using System;
using System.Collections.Generic;
using Silksprite.MeshWeaver.Models.DataObjects;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models.Meshes
{
    public class BakedMeshieFactory : IMeshieFactory
    {
        readonly Dictionary<LodMaskLayer, MeshieData> _data;
        readonly MeshieData _defaultData;
        readonly Func<int, Material> _materialMapping;

        public BakedMeshieFactory(Dictionary<LodMaskLayer, MeshieData> data, Func<int, Material> materialMapping)
        {
            _data = data;
            _materialMapping = materialMapping;
        }

        public BakedMeshieFactory(MeshieData data, Func<int, Material> materialMapping)
        {
            _data = new Dictionary<LodMaskLayer, MeshieData>();
            _defaultData = data;
            _materialMapping = materialMapping;
        }

        public Meshie Build(LodMaskLayer lod)
        {
            if (!_data.TryGetValue(lod, out var data))
            {
                data = _defaultData;
                if (data == null) return Meshie.Empty();
            }

            return data.ToMeshie(_materialMapping);
        }

        public Pathie Extract(string pathName, LodMaskLayer lod) => Pathie.Empty();
    }
}