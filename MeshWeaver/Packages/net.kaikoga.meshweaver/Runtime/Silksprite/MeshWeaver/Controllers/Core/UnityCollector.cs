using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Core
{
    public class UnityCollector<T>
    where T : Component
    {
        public T[] Collect(IEnumerable<T> source) => source.Where(item => item).ToArray();
    }
}