using System.Collections.Generic;
using Silksprite.MeshBuilder.Controllers.Base;
using Silksprite.MeshBuilder.Models;

namespace Silksprite.MeshBuilder.Controllers.Meshes
{
    public class CompositeMeshProvider : MeshProvider, ICompositeGeometry<MeshProvider>
    {
        public List<MeshProvider> meshProviders = new List<MeshProvider>();

        public List<MeshProvider> PrimaryElements { set => meshProviders = value; }

        protected override Meshie GenerateMeshie(LodMask lod)
        {
            var meshie = new Meshie();
            CollectMeshies(meshProviders, lod, meshie);
            return meshie;
        }
    }
}