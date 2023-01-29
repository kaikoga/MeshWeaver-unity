using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Core
{
    public class UnityCollector<T>
    where T : Component
    {
        readonly List<T> _content = new List<T>();
        int _revision;

        public T[] Value => _content.ToArray();

        public int Sync(IEnumerable<T> sources)
        {
            var lastCount = _content.Count;
            _content.Clear();
            _content.AddRange(sources);
            if (lastCount != _content.Count || _content.Any(content => MeshWeaverApplication.IsSelected(content.gameObject)))
            {
                _revision = MeshWeaverApplication.EmitRevision();
            }
            return _revision;
        }
    }
}