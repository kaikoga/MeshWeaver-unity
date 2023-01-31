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

    public sealed class Mux<T> : IEnumerable<MuxLayer<T>>, IEquatable<Mux<T>>
    {
        readonly MuxLayer<T>[] _layers;
        readonly int _offset; // TODO: possibly remove offset "optimization"

        public T Value => ValueAt(0);
        public T ValueAt(int channel)
        {
            channel += _offset;
            var value = default(T);
            for (var i = 0; i < _layers.Length; i++)
            {
                if (_layers[i].Channel <= channel) value = _layers[i].Value;
            }
            return value;
        }

        public int Count => _layers.Length;
        public int ChannelAtIndex(int index) => _layers[index].Channel;
        public MuxLayer<T> MuxLayerAtIndex(int index) => new MuxLayer<T>(_layers[index].Value, _layers[index].Channel - _offset);

        Mux(MuxLayer<T>[] layers, int offset)
        {
            _layers = layers;
            _offset = offset;
        }

        static readonly SortedList<int, T> WorkSortedList = new SortedList<int, T>();
        public static Mux<T> Build(IEnumerable<MuxLayer<T>> unsortedLayers)
        {
            var sortedList = WorkSortedList;
            foreach (var layer in unsortedLayers) sortedList[layer.Channel] = layer.Value;
            var compactedLayers = sortedList.Select(kv => new MuxLayer<T>(kv.Value, kv.Key)).ToArray();
            sortedList.Clear();
            return new Mux<T>(compactedLayers, 0);
        }

        public static Mux<T> FastBuild() => new Mux<T>(Array.Empty<MuxLayer<T>>(), 0);
        public static Mux<T> FastBuild(MuxLayer<T> singleLayer) => new Mux<T>(new[] { singleLayer }, 0 );

        public static Mux<T> FastBuild(IEnumerable<MuxLayer<T>> sortedLayers)
        {
            return new Mux<T>(sortedLayers.ToArray(), 0);
        }

        public Mux<T> Shift(int deltaChannel)
        {
            return new Mux<T>(_layers, _offset + deltaChannel);
        }

        public IEnumerator<MuxLayer<T>> GetEnumerator()
        {
            return _layers.Select(layer => new MuxLayer<T>(layer.Value, layer.Channel - _offset)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _layers.Select(layer => new MuxLayer<T>(layer.Value, layer.Channel - _offset)).GetEnumerator();
        }

        #region IEquatable<Mux<T>>

        public bool Equals(Mux<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return this.SequenceEqual(other);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is Mux<T> other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_layers.GetHashCode() * 397) ^ _offset;
            }
        }

        public static bool operator ==(Mux<T> left, Mux<T> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Mux<T> left, Mux<T> right)
        {
            return !Equals(left, right);
        }

        #endregion
    }

    public readonly struct MuxLayer<T> : IEquatable<MuxLayer<T>>
    {
        public readonly T Value;
        public readonly int Channel;

        public MuxLayer(T value, int channel)
        {
            Value = value;
            Channel = channel;
        }

        #region IEquatable<MuxLayer<T>>

        public bool Equals(MuxLayer<T> other)
        {
            return EqualityComparer<T>.Default.Equals(Value, other.Value) && Channel == other.Channel;
        }

        public override bool Equals(object obj)
        {
            return obj is MuxLayer<T> other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (EqualityComparer<T>.Default.GetHashCode(Value) * 397) ^ Channel;
            }
        }

        public static bool operator ==(MuxLayer<T> left, MuxLayer<T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(MuxLayer<T> left, MuxLayer<T> right)
        {
            return !left.Equals(right);
        }
        
        #endregion
    }
}