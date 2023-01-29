using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Extensions;
using Silksprite.MeshWeaver.Models.Meshes;

namespace Silksprite.MeshWeaver.Controllers.Core
{
    public class MeshieCollector
    {
        public IMeshieFactory CollectMeshie(MeshProvider meshProvider)
        {
            if (meshProvider == null) return MeshieFactory.Empty;

            var localTranslation = meshProvider.transform.ToLocalTranslation();
            return CompositeMeshieFactory.Builder().Concat(meshProvider.ToFactory(), localTranslation).ToFactory();
        }

        public IMeshieFactory CollectMeshies(IEnumerable<MeshProvider> meshProviders)
        {
            var builder = CompositeMeshieFactory.Builder();

            foreach (var meshProvider in meshProviders.Where(c => c != null && c.gameObject.activeSelf))
            {
                var localTranslation = meshProvider.transform.ToLocalTranslation();
                builder.Concat(meshProvider.ToFactory(), localTranslation);
            }

            return builder.ToFactory();
        }
    }
}