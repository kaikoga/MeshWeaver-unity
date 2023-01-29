using System.Collections.Generic;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Extensions;
using Silksprite.MeshWeaver.Models.Meshes;

namespace Silksprite.MeshWeaver.Controllers.Core
{
    public class MeshieCollector
    {
        public IMeshieFactory CollectMeshie(MeshProvider meshProvider) => meshProvider.CollectMeshie();

        public IMeshieFactory CollectMeshies(IEnumerable<MeshProvider> meshProviders) => meshProviders.CollectMeshies();
    }
}