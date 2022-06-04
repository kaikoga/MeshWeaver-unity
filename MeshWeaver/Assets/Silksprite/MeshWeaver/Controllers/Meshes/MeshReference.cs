using System.Collections.Generic;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Context;
using Silksprite.MeshWeaver.Models.Meshes;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    public class MeshReference : MeshProvider
    {
        public List<MeshProvider> meshProviders = new List<MeshProvider>();

        protected override IMeshieFactory CreateFactory(IMeshContext context)
        {
            return CollectMeshies(context, meshProviders);
        }
    }
}