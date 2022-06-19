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
        readonly SortedList<int, T> _layers;
        readonly int _offset;

        public T Value => ValueAt(0);
        public T ValueAt(int channel)
        {
            channel += _offset;
            var value = default(T);
            for (var i = 0; i < _layers.Count; i++)
            {
                if (_layers.Keys[i] <= channel) value = _layers.Values[i];
            }
            return value;
        }

        public int Count => _layers.Count;
        public int ChannelAtIndex(int index) => _layers.Keys[index];
        public MuxLayer<T> MuxLayerAtIndex(int index) => new MuxLayer<T>(_layers.Values[index], _layers.Keys[index]);

        Mux(SortedList<int, T> layers, int offset)
        {
            _layers = layers;
            _offset = offset;
        }

        public static Mux<T> Build(IEnumerable<MuxLayer<T>> layers)
        {
            var sortedList = new SortedList<int, T>();
            foreach (var layer in layers) sortedList[layer.Channel] = layer.Value; 
            return new Mux<T>(sortedList, 0);
        }

        public static Mux<T> FastBuild() => new Mux<T>(new SortedList<int, T>(), 0);
        public static Mux<T> FastBuild(MuxLayer<T> singleLayer) => new Mux<T>(new SortedList<int, T> { [singleLayer.Channel] = singleLayer.Value }, 0 );

        public static Mux<T> FastBuild(IEnumerable<MuxLayer<T>> layers)
        {
            var sortedList = new SortedList<int, T>();
            foreach (var layer in layers) sortedList.Add(layer.Channel, layer.Value); 
            return new Mux<T>(sortedList, 0);
        }

        public Mux<T> Shift(int deltaChannel)
        {
            return new Mux<T>(_layers, _offset + deltaChannel);
        }

        public IEnumerator<MuxLayer<T>> GetEnumerator()
        {
            return _layers.Select(layer => new MuxLayer<T>(layer.Value, layer.Key - _offset)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _layers.Select(layer => new MuxLayer<T>(layer.Value, layer.Key - _offset)).GetEnumerator();
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