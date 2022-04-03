using System;
using System.Collections.Generic;
using System.Linq;

namespace Silksprite.MeshBuilder.Models.Extensions
{
    public static class MuxExtension
    {
        public static Mux<T> AddLayer<T>(this Mux<T> self, T value, int channel)
        {
            return new Mux<T>(self.Concat(new[] { new MuxLayer<T>(value, channel) }));
        }

        public static Mux<TOut> ZipMux<T1, T2, TOut>(this Mux<T1> self, Mux<T2> other, Func<T1, T2, TOut> selector)
        {
            return self.ZipMux(other, (a, b, _) => selector(a, b));
        }

        public static Mux<TOut> ZipMux<T1, T2, TOut>(this Mux<T1> self, Mux<T2> other, Func<T1, T2, int, TOut> selector)
        {
            return new Mux<TOut>(self.ZipMuxLayers(other, selector));
        }

        static IEnumerable<MuxLayer<TOut>> ZipMuxLayers<T1, T2, TOut>(this Mux<T1> self, Mux<T2> other, Func<T1, T2, int, TOut> selector)
        {
            MuxLayer<TOut> ZipChannel(MuxLayer<T1> curSelf, MuxLayer<T2> curOther)
            {
                var channel = Math.Max(curSelf.Channel, curOther.Channel);
                return new MuxLayer<TOut>(selector(curSelf.Value, curOther.Value, channel), channel);
            }

            using (var itSelf = self.OrderBy(layer => layer.Channel).GetEnumerator())
            using (var itOther = other.OrderBy(layer => layer.Channel).GetEnumerator())
            {
                MuxLayer<T1> lastSelf = default;
                MuxLayer<T2> lastOther = default;
                var hasSelf = itSelf.MoveNext();
                var hasOther = itOther.MoveNext();
                while (hasSelf || hasOther)
                {
                    if (!hasSelf)
                    {
                        lastOther = itOther.Current;
                        hasOther = itOther.MoveNext();
                        yield return ZipChannel(lastSelf, lastOther);
                        continue;
                    }
                    if (!hasOther)
                    {
                        lastSelf = itSelf.Current;
                        hasSelf = itSelf.MoveNext();
                        yield return ZipChannel(lastSelf, lastOther);
                        continue;
                    }

                    if (itSelf.Current.Channel < itOther.Current.Channel)
                    {
                        lastSelf = itSelf.Current;
                        hasSelf = itSelf.MoveNext();
                        yield return ZipChannel(lastSelf, lastOther);
                    }
                    else if (itSelf.Current.Channel > itOther.Current.Channel)
                    {
                        lastOther = itOther.Current;
                        hasOther = itOther.MoveNext();
                        yield return ZipChannel(lastSelf, lastOther);
                    }
                    else
                    {
                        lastSelf = itSelf.Current;
                        hasSelf = itSelf.MoveNext();
                        lastOther = itOther.Current;
                        hasOther = itOther.MoveNext();
                        yield return ZipChannel(lastSelf, lastOther);
                    }
                }
            }
        }

        public static Mux<T> SelectMuxChannels<T>(this Mux<T> self, Func<int, int> selector)
        {
            return new Mux<T>(self.Select(layer => new MuxLayer<T>(layer.Value, selector(layer.Channel))));
        }

        public static Mux<TOut> SelectMuxValues<TIn, TOut>(this Mux<TIn> self, Func<TIn, TOut> selector)
        {
            return new Mux<TOut>(self.Select(layer => new MuxLayer<TOut>(selector(layer.Value), layer.Channel)));
        }

        public static Mux<TOut> SelectMux<TIn, TOut>(this Mux<TIn> self, Func<TIn, int, TOut> valueSelector, Func<TIn, int, int> channelSelector)
        {
            return new Mux<TOut>(self.Select(layer => new MuxLayer<TOut>(valueSelector(layer.Value, layer.Channel), channelSelector(layer.Value, layer.Channel))));
        }
    }
}