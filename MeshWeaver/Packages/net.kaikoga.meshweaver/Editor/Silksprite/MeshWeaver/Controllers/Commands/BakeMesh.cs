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
                baked.lodMaskLayers = LodMaskLayers.Values;
                baked.meshData = LodMaskLayers.Values.Select(lod => MeshieData.FromMeshie(target.ToFactory(context).Build(lod), i => i)).ToArray();
                baked.materials = context.ToMaterials();
            }

            var bakedTransform = baked.transform;
            bakedTransform.localPosition = transform.localPosition;
            bakedTransform.localRotation = transform.localRotation;
            bakedTransform.localScale = transform.localScale;
        }
    }
}