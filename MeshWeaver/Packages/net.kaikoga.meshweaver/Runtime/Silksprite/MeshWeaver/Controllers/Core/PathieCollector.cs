using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Extensions;
using Silksprite.MeshWeaver.Models.Modifiers;
using Silksprite.MeshWeaver.Models.Paths;
using Silksprite.MeshWeaver.Models.Paths.Core;

namespace Silksprite.MeshWeaver.Controllers.Core
{
    public class PathieCollector
    {
        public IPathieFactory CollectPathie(PathProvider pathProvider)
        {
            if (pathProvider == null) return PathieFactory.Empty;

            var localTranslation = pathProvider.transform.ToLocalTranslation();
            // FIXME: maybe we want to cleanup Pathie.isLoop and fix this 
            return ModifiedPathieFactory.Builder(pathProvider.ToFactory(), pathProvider.lodMask).Concat(new VertwiseTranslate(localTranslation)).ToFactory();
        }

        public IPathieFactory CollectPathies(IEnumerable<PathProvider> pathProviders, bool isLoop, bool smoothJoin)
        {
            var builder = CompositePathieFactory.Builder(isLoop, smoothJoin);
            
            foreach (var pathProvider in pathProviders.Where(c => c != null && c.gameObject.activeSelf))
            {
                var localTranslation = pathProvider.transform.ToLocalTranslation();
                builder.Concat(pathProvider.ToFactory(), localTranslation);
            }

            return builder.ToFactory();
        }
    }
}