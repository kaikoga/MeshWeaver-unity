using Silksprite.MeshWeaver.Models;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Context;
using Silksprite.MeshWeaver.Models.Meshes;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    public class PolygonMeshProvider : MeshProvider
    {
        public PathProvider pathProvider;

        public Material material;

        protected override IMeshieFactory CreateFactory(IMeshContext context)
        {
            return new PolygonMeshieFactory2(CollectPathie(pathProvider), context.GetMaterialIndex(material));
        }
    }
}