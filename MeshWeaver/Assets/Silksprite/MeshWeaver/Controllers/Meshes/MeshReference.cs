using System.Collections.Generic;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Models.Meshes;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    public class MeshReference : MeshProvider
    {
        public List<MeshProvider> meshProviders = new List<MeshProvider>();

        protected override IMeshieFactory CreateFactory()
        {
            return CollectMeshies(meshProviders);
        }
    }
}