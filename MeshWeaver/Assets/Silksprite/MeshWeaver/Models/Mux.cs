using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Silksprite.MeshWeaver.Models
{
    public static class Mux
    {
        public static Mux<T> Empty<T>() => Mux<T>.FastBuild();

        public static Mux<T> Single<T>(T singleValue) => Mux<T>.FastBuild(new MuxLayer<T>(singleValue, 0));

        public static Mux<T> Build<T>(IEnumerable<MuxLayer<T>> layers) => Mux<T>.Build(layers);
    }

    public class Mux<T> : IEnumerable<MuxLayer<T>>
    {
        readonly MuxLayer<T>[] _layers;
        readonly int _offset;

        public T Value => ValueAt(0);
        public T ValueAt(int channel)
        {
            channel += _offset;
            return _layers.LastOrDefault(layer => layer.Channel <= channel).Value;
        }

        Mux(MuxLayer<T>[] layers, int offset)
        {
            _layers = layers;
            _offset = offset;
        }

        public static Mux<T> Build(IEnumerable<MuxLayer<T>> layers)
        {
            return new Mux<T>(layers.GroupBy(layer => layer.Channel).Select(g => g.Last()).ToArray(), 0);
        }

        public static Mux<T> FastBuild() => new Mux<T>(new MuxLayer<T>[] { }, 0);
        public static Mux<T> FastBuild(MuxLayer<T> singleLayer) => new Mux<T>(new[] { singleLayer }, 0 );

        public static Mux<T> FastBuild(IEnumerable<MuxLayer<T>> layers)
        {
            var array = layers.ToArray();
            for (var i = 0; i < array.Length; i++)
            for (var j = i; j < array.Length; j++)
            {
                if (i == j) continue;
                if (array[i].Channel == array[j].Channel) throw new ArgumentException("Duplicate channel found in Mux<T>.FastBuild()");
            }
            return new Mux<T>(array, 0);
        }

        public Mux<T> Shift(int deltaChannel)
        {
            return new Mux<T>(_layers, _offset + deltaChannel);
        }

        public IEnumerator<MuxLayer<T>> GetEnumerator()
        {
            return _offset == 0 ? ((IEnumerable<MuxLayer<T>>)_layers).GetEnumerator() : _layers.Select(layer => new MuxLayer<T>(layer.Value, layer.Channel - _offset)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _offset == 0 ? _layers.GetEnumerator() : _layers.Select(layer => new MuxLayer<T>(layer.Value, layer.Channel - _offset)).GetEnumerator();
        }
    }

    public readonly struct MuxLayer<T>
    {
        public readonly T Value;
        public readonly int Channel;

        public MuxLayer(T value, int channel)
        {
            Value = value;
            Channel = channel;
        }
    }
}