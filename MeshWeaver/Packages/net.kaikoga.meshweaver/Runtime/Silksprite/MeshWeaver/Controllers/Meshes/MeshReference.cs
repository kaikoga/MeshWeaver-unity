using System.Collections.Generic;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Core;
using Silksprite.MeshWeaver.Models.Meshes;

namespace Silksprite.MeshWeaver.Controllers.Meshes
{
    public class MeshReference : MeshProvider
    {
        public List<MeshProvider> meshProviders = new List<MeshProvider>();
        readonly MeshieCollector _meshProvidersCollector = new MeshieCollector();

        protected override IMeshieFactory CreateFactory()
        {
            return _meshProvidersCollector.CollectMeshies(meshProviders);
        }
    }
}