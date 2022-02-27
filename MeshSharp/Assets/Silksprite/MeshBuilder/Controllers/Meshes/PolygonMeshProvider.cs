using System.Collections.Generic;
using Silksprite.MeshBuilder.Models;
using Silksprite.MeshBuilder.Models.Meshes;

namespace Silksprite.MeshBuilder.Controllers.Meshes
{
    public class PolygonMeshProvider : MeshProvider
    {
        public List<PathProvider> pathProviders = new List<PathProvider>();

        public override Meshie ToMeshie()
        {
            return new PolygonMeshieFactory().Build(CollectPathies(pathProviders, new Pathie()), new Meshie());
        }
    }
}