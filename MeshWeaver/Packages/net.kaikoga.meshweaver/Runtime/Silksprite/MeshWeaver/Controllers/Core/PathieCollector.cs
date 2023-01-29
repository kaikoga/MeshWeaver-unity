using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Extensions;
using Silksprite.MeshWeaver.Models.Meshes;
using Silksprite.MeshWeaver.Models.Paths;

namespace Silksprite.MeshWeaver.Controllers.Core
{
    public class PathieCollector
    {
        public IPathieFactory CollectPathie(PathProvider pathProvider) => pathProvider.CollectPathie();

        public IPathieFactory CollectPathies(IEnumerable<PathProvider> pathProviders, bool isLoop, bool smoothJoin) => pathProviders.CollectPathies(isLoop, smoothJoin);
    }
}