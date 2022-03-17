using System;
using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshBuilder.Models;

namespace Silksprite.MeshBuilder.Extensions
{
    public static class MuxExtension
    {
        public static Mux<T> AddLayer<T>(this Mux<T> self, T value, int minIndex)
        {
            return new Mux<T>(self.Concat(new[] { new MuxLayer<T>(value, minIndex) }));
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
                var minIndex = Math.Max(curSelf.MinIndex, curOther.MinIndex);
                return new MuxLayer<TOut>(selector(curSelf.Value, curOther.Value, minIndex), minIndex);
            }

            using (var itSelf = self.OrderBy(ch => ch.MinIndex).GetEnumerator())
            using (var itOther = other.OrderBy(ch => ch.MinIndex).GetEnumerator())
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

                    if (itSelf.Current.MinIndex < itOther.Current.MinIndex)
                    {
                        lastSelf = itSelf.Current;
                        hasSelf = itSelf.MoveNext();
                        yield return ZipChannel(lastSelf, lastOther);
                    }
                    else if (itSelf.Current.MinIndex > itOther.Current.MinIndex)
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
            return new Mux<T>(self.Select(ch => new MuxLayer<T>(ch.Value, selector(ch.MinIndex))));
        }

        public static Mux<TOut> SelectMuxValues<TIn, TOut>(this Mux<TIn> self, Func<TIn, TOut> selector)
        {
            return new Mux<TOut>(self.Select(ch => new MuxLayer<TOut>(selector(ch.Value), ch.MinIndex)));
        }
    }
}