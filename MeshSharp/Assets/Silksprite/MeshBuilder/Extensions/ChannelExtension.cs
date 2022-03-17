using System;
using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshBuilder.Models;

namespace Silksprite.MeshBuilder.Extensions
{
    public static class ChannelExtension
    {
        public static IEnumerable<Channel<TOut>> ZipChannels<T1, T2, TOut>(this IEnumerable<Channel<T1>> self, IEnumerable<Channel<T2>> other, Func<T1, T2, int, TOut> selector)
        {
            if (!self.Any() || !other.Any()) yield break;
            var t1 = self.First().Value;
            var t2 = other.First().Value;
            yield return new Channel<TOut>(selector(t1, t2, 0), 0);
        }
    }
}