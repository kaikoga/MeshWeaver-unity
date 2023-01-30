using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshWeaver.Controllers.Base.Modifiers;

namespace Silksprite.MeshWeaver.Controllers.Core
{
    public class ModifierCollector<T>
    where T : IModifierProvider
    {
        readonly List<T> _content = new List<T>();
        int _revision;

        public T[] Value => _content.ToArray();

        public int Sync(IEnumerable<T> sources)
        {
            _content.Clear();
            _content.AddRange(sources);
            _revision = _content.Aggregate(0, (r, content) => r ^ content.Revision);
            return _revision;
        }
    }
}