using System;
using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshBuilder.Models;

namespace Silksprite.MeshBuilder.Extensions
{
    public static class ChannelExtension
    {
        public static IEnumerable<Channel<TOut>> ZipChannels<T1, T2, TOut>(this IEnumerable<Channel<T1>> self, IEnumerable<Channel<T2>> other, Func<T1, T2, TOut> selector)
        {
            return self.ZipChannels(other, (a, b, _) => selector(a, b));
        }

        public static IEnumerable<Channel<TOut>> ZipChannels<T1, T2, TOut>(this IEnumerable<Channel<T1>> self, IEnumerable<Channel<T2>> other, Func<T1, T2, int, TOut> selector)
        {
            return self.ZipChannelsInternal(other, selector);
        }
        static IEnumerable<Channel<TOut>> ZipChannelsInternal<T1, T2, TOut>(this IEnumerable<Channel<T1>> self, IEnumerable<Channel<T2>> other, Func<T1, T2, int, TOut> selector)
        {
            Channel<TOut> ZipChannel(Channel<T1> curSelf, Channel<T2> curOther)
            {
                var minIndex = Math.Max(curSelf.MinIndex, curOther.MinIndex);
                return new Channel<TOut>(selector(curSelf.Value, curOther.Value, minIndex), minIndex);
            }

            using (var itSelf = self.OrderBy(ch => ch.MinIndex).GetEnumerator())
            using (var itOther = other.OrderBy(ch => ch.MinIndex).GetEnumerator())
            {
                Channel<T1> lastSelf = default;
                Channel<T2> lastOther = default;
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

        public static IEnumerable<Channel<TOut>> SelectChannelValues<TIn, TOut>(this IEnumerable<Channel<TIn>> self, Func<TIn, TOut> selector)
        {
            return self.SelectChannelValues((v, _) => selector(v));
        }

        static IEnumerable<Channel<TOut>> SelectChannelValues<TIn, TOut>(this IEnumerable<Channel<TIn>> self, Func<TIn, int, TOut> selector)
        {
            return self.Select(ch => new Channel<TOut>(selector(ch.Value, ch.MinIndex), ch.MinIndex));
        }
    }
}