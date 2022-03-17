using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Silksprite.MeshBuilder.Models
{
    public class Mux<T> : IEnumerable<MuxLayer<T>>
    {
        readonly MuxLayer<T>[] _layers;

        public Mux(IEnumerable<MuxLayer<T>> layers)
        {
            _layers = layers.ToArray();
        }

        public IEnumerator<MuxLayer<T>> GetEnumerator()
        {
            return ((IEnumerable<MuxLayer<T>>)_layers).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _layers.GetEnumerator();
        }
    }

    public readonly struct MuxLayer<T>
    {
        public readonly T Value;
        public readonly int MinIndex;

        public MuxLayer(T value, int minIndex)
        {
            Value = value;
            MinIndex = minIndex;
        }
    }

}