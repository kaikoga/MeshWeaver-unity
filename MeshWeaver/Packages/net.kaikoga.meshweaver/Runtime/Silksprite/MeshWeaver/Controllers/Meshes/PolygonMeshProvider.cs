using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Core;
using Silksprite.MeshWeaver.Models.Meshes;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    public class PolygonMeshProvider : MeshProvider
    {
        public PathProvider pathProvider;
        readonly PathieCollector _pathProviderCollector = new PathieCollector();

        public Material material;

        protected override IMeshieFactory CreateFactory()
        {
            return new PolygonMeshieFactory(_pathProviderCollector.CollectPathie(pathProvider), material);
        }
    }
}