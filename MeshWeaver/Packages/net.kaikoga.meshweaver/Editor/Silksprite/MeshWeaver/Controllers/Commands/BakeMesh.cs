using System;
using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Context;
using Silksprite.MeshWeaver.Controllers.Extensions;
using Silksprite.MeshWeaver.Controllers.Meshes;
using Silksprite.MeshWeaver.Models;
using Silksprite.MeshWeaver.Models.DataObjects;
using Silksprite.MeshWeaver.Utils;
using UnityEngine;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Commands
{
    public class BakeMesh : ICommand<MeshProvider>
    {
        public LocalizedContent Name => Loc("Bake Mesh");

        public void Invoke(MeshProvider target)
        {
            var transform = target.transform;
            var baked = transform.parent.AddChildComponent<BakedMeshProvider>();
            var context = new MeshIndexMapping();
            baked.bakedData = LodMaskLayers.Values.Select(lod =>
            {
                var meshie = target.ToFactory(NullMeshContext.Instance).Build(lod);
                return new BakedMeshieData
                {
                    lodMaskLayers = new[] { lod },
                    meshData = MeshieData.FromMeshie(meshie, material => context.GetMaterialIndex(material))
                };
            }).ToArray();
            baked.materials = context.ToMaterials();

            var bakedTransform = baked.transform;
            bakedTransform.localPosition = transform.localPosition;
            bakedTransform.localRotation = transform.localRotation;
            bakedTransform.localScale = transform.localScale;
        }
        
        sealed class MeshIndexMapping
        {
            readonly List<Material> _materials;

            public MeshIndexMapping() => _materials = new List<Material>();

            public Material[] ToMaterials() => _materials.ToArray();

            public int GetMaterialIndex(Material material)
            {
                if (!material) 
                {
                    if (_materials.Count == 0) _materials.Add(null);
                    return 0;
                }

                var index = _materials.IndexOf(material);
                if (index >= 0) return index;

                _materials.Add(material);
                return _materials.Count - 1;
            }
        }

    }
}