using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Silksprite.MeshBuilder.Models
{
    public static class Mux
    {
        public static Mux<T> Empty<T>() => new Mux<T>(new MuxLayer<T>[] { });

        public static Mux<T> Single<T>(T singleValue) => new Mux<T>(new[] { new MuxLayer<T>(singleValue, 0) });
    }

    public class Mux<T> : IEnumerable<MuxLayer<T>>
    {
        readonly MuxLayer<T>[] _layers;

        public T Value => _layers.LastOrDefault(layer => layer.MinIndex <= 0).Value;

        public Mux(IEnumerable<MuxLayer<T>> layers) : this(layers.ToArray()) { }

        Mux(MuxLayer<T>[] layers)
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