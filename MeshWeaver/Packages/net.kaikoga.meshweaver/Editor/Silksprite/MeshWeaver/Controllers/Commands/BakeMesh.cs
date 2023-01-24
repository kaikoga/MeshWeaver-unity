using System.Linq;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Context;
using Silksprite.MeshWeaver.Controllers.Extensions;
using Silksprite.MeshWeaver.Controllers.Meshes;
using Silksprite.MeshWeaver.Models;
using Silksprite.MeshWeaver.Models.DataObjects;
using Silksprite.MeshWeaver.Utils;
using UnityEditor;
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
            using (var context = new DynamicMeshContext())
            {
                baked.bakedData = LodMaskLayers.Values.Select(lod => new BakedMeshieData
                {
                    lodMaskLayers = new [] { lod },
                    meshData = MeshieData.FromMeshie(target.ToFactory(context).Build(lod), material => context.GetMaterialIndex(material))
                }).ToArray();
                baked.materials = context.ToMaterials();
            }

            var bakedTransform = baked.transform;
            bakedTransform.localPosition = transform.localPosition;
            bakedTransform.localRotation = transform.localRotation;
            bakedTransform.localScale = transform.localScale;
        }
    }
}