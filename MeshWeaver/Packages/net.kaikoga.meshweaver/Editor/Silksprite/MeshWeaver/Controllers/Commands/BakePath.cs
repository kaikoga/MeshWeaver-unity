using System.Linq;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Context;
using Silksprite.MeshWeaver.Controllers.Extensions;
using Silksprite.MeshWeaver.Controllers.Paths;
using Silksprite.MeshWeaver.Models;
using Silksprite.MeshWeaver.Models.DataObjects;
using Silksprite.MeshWeaver.Utils;
using UnityEditor;
using UnityEngine;
using static Silksprite.MeshWeaver.Tools.LocalizationTool;

namespace Silksprite.MeshWeaver.Controllers.Commands
{
    public class BakePath : ICommand<PathProvider>
    {
        public LocalizedContent Name => Loc("Bake Path");

        public void Invoke(PathProvider target)
        {
            var transform = target.transform;
            var baked = transform.parent.AddChildComponent<BakedPathProvider>();
            using (var context = new DynamicMeshContext())
            {
                baked.bakedData = LodMaskLayers.Values.Select(lod => new BakedPathieData
                {
                    lodMaskLayers = new[] { lod },
                    pathData = PathieData.FromPathie(target.ToFactory(context).Build(lod))
                }).ToArray();
            }

            var bakedTransform = baked.transform;
            bakedTransform.localPosition = transform.localPosition;
            bakedTransform.localRotation = transform.localRotation;
            bakedTransform.localScale = transform.localScale;
        }
    }
}