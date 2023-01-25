using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Extensions;
using Silksprite.MeshWeaver.Models.Meshes;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    public class PolygonMeshProvider : MeshProvider
    {
        public PathProvider pathProvider;

        public Material material;

        protected override IMeshieFactory CreateFactory()
        {
            return new PolygonMeshieFactory(pathProvider.CollectPathie(), material);
        }
    }
}