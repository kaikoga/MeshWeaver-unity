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

        public override int Sync() => _pathProviderCollector.Sync(pathProvider);

        protected override IMeshieFactory CreateFactory() => new PolygonMeshieFactory(_pathProviderCollector.SingleValue(), material);
    }
}