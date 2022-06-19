using System;
using System.Collections.Generic;
using System.Linq;

namespace Silksprite.MeshWeaver.Models.Extensions
{
    public static class MuxExtension
    {
        public static Mux<T> AddLayer<T>(this Mux<T> self, T value, int channel)
        {
            return Mux<T>.Build(self.Concat(new[] { new MuxLayer<T>(value, channel) }));
        }

        public static Mux<TOut> ZipMux<T1, T2, TOut>(this Mux<T1> self, Mux<T2> other, Func<T1, T2, TOut> selector)
        {
            return self.ZipMux(other, (a, b, _) => selector(a, b));
        }

        public static Mux<TOut> ZipMux<T1, T2, TOut>(this Mux<T1> self, Mux<T2> other, Func<T1, T2, int, TOut> selector)
        {
            return Mux<TOut>.FastBuild(self.ZipMuxLayers(other, selector));
        }

        static IEnumerable<MuxLayer<TOut>> ZipMuxLayers<T1, T2, TOut>(this Mux<T1> self, Mux<T2> other, Func<T1, T2, int, TOut> selector)
        {
            MuxLayer<TOut> ZipChannel(MuxLayer<T1> curSelf, MuxLayer<T2> curOther)
            {
                var channel = Math.Max(curSelf.Channel, curOther.Channel);
                return new MuxLayer<TOut>(selector(curSelf.Value, curOther.Value, channel), channel);
            }

            var countSelf = self.Count;
            var countOther = other.Count;
            var indexSelf = 0;
            var indexOther = 0;
            var hasSelf = indexSelf < countSelf;
            var hasOther = indexOther < countOther;
            var lastSelf = hasSelf ? self.MuxLayerAtIndex(0) : default;
            var lastOther = hasOther ? other.MuxLayerAtIndex(0) : default;
            while (hasSelf && hasOther)
            {
                var compare = self.ChannelAtIndex(indexSelf) - other.ChannelAtIndex(indexOther); 
                if (compare < 0) // self.ChannelAtIndex(indexSelf) < other.ChannelAtIndex(indexOther)
                {
                    lastSelf = self.MuxLayerAtIndex(indexSelf++);
                    hasSelf = indexSelf < countSelf;
                    yield return ZipChannel(lastSelf, lastOther);
                }
                else if (compare > 0) // self.ChannelAtIndex(indexSelf) > other.ChannelAtIndex(indexOther)
                {
                    lastOther = other.MuxLayerAtIndex(indexOther++);
                    hasOther = indexOther < countOther;
                    yield return ZipChannel(lastSelf, lastOther);
                }
                else
                {
                    lastSelf = self.MuxLayerAtIndex(indexSelf++);
                    hasSelf = indexSelf < countSelf;
                    lastOther = other.MuxLayerAtIndex(indexOther++);
                    hasOther = indexOther < countOther;
                    yield return ZipChannel(lastSelf, lastOther);
                }
            }
            while (hasOther)
            {
                lastOther = other.MuxLayerAtIndex(indexOther++);
                hasOther = indexOther < countOther;
                yield return ZipChannel(lastSelf, lastOther);
            }
            while (hasSelf)
            {
                lastSelf = self.MuxLayerAtIndex(indexSelf++);
                hasSelf = indexSelf < countSelf;
                yield return ZipChannel(lastSelf, lastOther);
            }
        }

        public static Mux<T> SelectMuxChannels<T>(this Mux<T> self, Func<int, int> selector)
        {
            return Mux<T>.Build(self.Select(layer => new MuxLayer<T>(layer.Value, selector(layer.Channel))));
        }

        public static Mux<TOut> SelectMuxValues<TIn, TOut>(this Mux<TIn> self, Func<TIn, TOut> selector)
        {
            return Mux<TOut>.FastBuild(self.Select(layer => new MuxLayer<TOut>(selector(layer.Value), layer.Channel)));
        }

        public static Mux<TOut> SelectMux<TIn, TOut>(this Mux<TIn> self, Func<TIn, int, TOut> valueSelector, Func<TIn, int, int> channelSelector)
        {
            return Mux<TOut>.Build(self.Select(layer => new MuxLayer<TOut>(valueSelector(layer.Value, layer.Channel), channelSelector(layer.Value, layer.Channel))));
        }

        public static Mux<T> WithMuxChannelValue<T>(this Mux<T> self, int channel, Func<T, T> selector)
        {
            return self.SelectMux(
                (value, ch) => ch != channel ? value : selector(value),
                (value, ch) => ch);
        }
    }
}