using System;
using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshBuilder.Models;

namespace Silksprite.MeshBuilder.Extensions
{
    public static class MuxExtension
    {
        public static T Value<T>(this IEnumerable<MuxLayer<T>> ch)
        {
            return ch.LastOrDefault(uv => uv.MinIndex <= 0).Value;
        }
        
        public static IEnumerable<MuxLayer<TOut>> ZipChannels<T1, T2, TOut>(this IEnumerable<MuxLayer<T1>> self, IEnumerable<MuxLayer<T2>> other, Func<T1, T2, TOut> selector)
        {
            return self.ZipChannels(other, (a, b, _) => selector(a, b));
        }

        public static IEnumerable<MuxLayer<TOut>> ZipChannels<T1, T2, TOut>(this IEnumerable<MuxLayer<T1>> self, IEnumerable<MuxLayer<T2>> other, Func<T1, T2, int, TOut> selector)
        {
            return self.ZipChannelsInternal(other, selector);
        }
        static IEnumerable<MuxLayer<TOut>> ZipChannelsInternal<T1, T2, TOut>(this IEnumerable<MuxLayer<T1>> self, IEnumerable<MuxLayer<T2>> other, Func<T1, T2, int, TOut> selector)
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

        public static IEnumerable<MuxLayer<TOut>> SelectMuxValues<TIn, TOut>(this IEnumerable<MuxLayer<TIn>> self, Func<TIn, TOut> selector)
        {
            return self.SelectMuxValues((v, _) => selector(v));
        }

        static IEnumerable<MuxLayer<TOut>> SelectMuxValues<TIn, TOut>(this IEnumerable<MuxLayer<TIn>> self, Func<TIn, int, TOut> selector)
        {
            return self.Select(ch => new MuxLayer<TOut>(selector(ch.Value, ch.MinIndex), ch.MinIndex));
        }
    }
}