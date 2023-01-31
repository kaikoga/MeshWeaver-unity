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
        readonly List<PathProvider> _content = new List<PathProvider>();
        int _revision;

        public IPathieFactory SingleValue()
        {
            if (_content.Count == 0) return PathieFactory.Empty;

            var pathProvider = _content[0];

            var localTranslation = pathProvider.transform.ToLocalTranslation();
            // FIXME: maybe we want to cleanup Pathie.isLoop and fix this 
            return ModifiedPathieFactory.Builder(pathProvider.ToFactory(), pathProvider.lodMask).Concat(new VertwiseTranslate(localTranslation)).ToFactory();
        }

        public IPathieFactory CompositeValue(bool isLoop, bool smoothJoin)
        {
            var builder = CompositePathieFactory.Builder(isLoop, smoothJoin);
            
            foreach (var pathProvider in _content)
            {
                var localTranslation = pathProvider.transform.ToLocalTranslation();
                builder.Concat(pathProvider.ToFactory(), localTranslation);
            }

            return builder.ToFactory();
        }

        public int Sync(PathProvider pathProvider)
        {
            Collectors.Start(this);
            _content.Clear();
            if (pathProvider) _content.Add(pathProvider);
            _revision = _content.Aggregate(0, (r, content) => r ^ content.Revision);
            Collectors.Finish(this);
            return _revision;
        }

        public int Sync(IEnumerable<PathProvider> pathProviders)
        {
            Collectors.Start(this);
            _content.Clear();
            _content.AddRange(pathProviders.Where(c => c != null && c.gameObject.activeSelf));
            _revision = _content.Aggregate(0, (r, content) => r ^ content.Revision);
            Collectors.Finish(this);
            return _revision;
        }
    }
}