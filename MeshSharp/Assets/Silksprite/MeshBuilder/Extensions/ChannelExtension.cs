using System;
using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshBuilder.Models;
using UnityEngine;

namespace Silksprite.MeshBuilder.Extensions
{
    public static class ChannelExtension
    {
        public static IEnumerable<TOutCh> ZipChannels<T1, T1Ch, T2, T2Ch, TOut, TOutCh>(this IEnumerable<T1Ch> self, IEnumerable<T2Ch> other, Func<T1, T2, int, TOutCh> selector)
            where T1Ch : Channel<T1>
            where T2Ch: Channel<T2>
            where TOutCh: Channel<TOut>
        {
            if (!self.Any() || !other.Any()) yield break;
            var t1 = self.First().Value;
            var t2 = other.First().Value;
            yield return selector(t1, t2, 0);
        }

        public static IEnumerable<UvChannel> ZipUvChannels(this IEnumerable<UvChannel> self, IEnumerable<UvChannel> other, Func<Vector2, Vector2, int, Vector2> selector)
        {
            return self.ZipChannels<Vector2, UvChannel, Vector2, UvChannel, Vector2, UvChannel>(other, (a, b, minValue) => new UvChannel(selector(a, b, minValue), minValue));
        }

    }
}