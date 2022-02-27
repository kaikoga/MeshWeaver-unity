using System.Collections.Generic;
using Silksprite.MeshBuilder.Models;
using Silksprite.MeshBuilder.Models.Meshes;

namespace Silksprite.MeshBuilder.Controllers.Meshes
{
    public class MatrixMeshProvider : MeshProvider
    {
        public List<PathProvider> pathProvidersX = new List<PathProvider>();
        public List<PathProvider> pathProvidersY = new List<PathProvider>();

        public override Meshie ToMeshie()
        {
            return new MatrixMeshieFactory().Build(CollectPathies(pathProvidersX, new Pathie(), false), CollectPathies(pathProvidersY, new Pathie(), false), new Meshie());
        }
    }
}