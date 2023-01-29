using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Extensions;
using Silksprite.MeshWeaver.Models.Meshes;

namespace Silksprite.MeshWeaver.Controllers.Core
{
    public class MeshieCollector
    {
        readonly List<MeshProvider> _content = new List<MeshProvider>();

        public IMeshieFactory Value
        {
            get
            {
                if (_content.Count == 0) return MeshieFactory.Empty;

                var builder = CompositeMeshieFactory.Builder();
                foreach (var meshProvider in _content)
                {
                    var localTranslation = meshProvider.transform.ToLocalTranslation();
                    builder.Concat(meshProvider.ToFactory(), localTranslation);
                }

                return builder.ToFactory();
            }
        }

        public void Sync(MeshProvider meshProvider)
        {
            _content.Clear();
            if (meshProvider) _content.Add(meshProvider);
        }

        public void Sync(IEnumerable<MeshProvider> meshProviders)
        {
            _content.Clear();
            _content.AddRange(meshProviders.Where(c => c != null && c.gameObject.activeSelf));
        }
    }
}