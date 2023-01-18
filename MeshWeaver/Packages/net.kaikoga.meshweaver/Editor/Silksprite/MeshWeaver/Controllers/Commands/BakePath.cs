using System.Linq;
using Silksprite.MeshWeaver.Controllers.Base;
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
            baked.lodMaskLayers = LodMaskLayers.Values;
            baked.pathData = LodMaskLayers.Values.Select(lod => PathieData.FromPathie(target.ToFactory().Build(lod))).ToArray();
            var bakedTransform = baked.transform;
            bakedTransform.localPosition = transform.localPosition;
            bakedTransform.localRotation = transform.localRotation;
            bakedTransform.localScale = transform.localScale;
        }
    }
}