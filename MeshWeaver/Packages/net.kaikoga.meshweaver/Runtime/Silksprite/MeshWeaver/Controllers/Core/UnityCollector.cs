using System.Collections.Generic;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Core
{
    public class UnityCollector<T>
    where T : Component
    {
        readonly List<T> _content = new List<T>();

        public T[] Value => _content.ToArray();

        public T[] Collect(IEnumerable<T> source)
        {
            Sync(source);
            return Value;
        }

        public void Sync(IEnumerable<T> source)
        {
            _content.Clear();
            _content.AddRange(source);
        }
    }
}