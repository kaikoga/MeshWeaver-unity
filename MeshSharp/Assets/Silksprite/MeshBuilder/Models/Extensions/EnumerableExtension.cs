using System;
using System.Collections.Generic;
using System.Linq;

namespace Silksprite.MeshBuilder.Models.Extensions
{
    public static class EnumerableExtension
    {
        public static IEnumerable<float> Integral(this IEnumerable<float> e)
        {
            var value = 0f;
            foreach (var v in e)
            {
                value += v;
                yield return value;
            }
        }

        public static IEnumerable<T> Dedup<T>(this IEnumerable<T> e)
        {
            return e.Dedup((a, b) => Equals(a, b));
        }

        public static IEnumerable<T> DedupLoop<T>(this IEnumerable<T> e)
        {
            return e.DedupLoop((a, b) => Equals(a, b));
        }

        public static IEnumerable<T> Dedup<T>(this IEnumerable<T> e, Func<T, T, bool> equality)
        {
            return e.Take(1).Concat(
                e.Pairwise((a, b) => equality(a, b) ? Enumerable.Empty<T>() : Enumerable.Repeat(b, 1))
                    .SelectMany(x => x));
        }

        public static IEnumerable<T> DedupLoop<T>(this IEnumerable<T> e, Func<T, T, bool> equality)
        {
            return equality(e.First(), e.Last()) ? e.Skip(1).Dedup(equality) : e.Dedup(equality);
        }

        public static IEnumerable<TResult> Pairwise<TSource, TResult>(this IEnumerable<TSource> e, Func<TSource, TSource, TResult> selector)
        {
            return e.Zip(e.Skip(1), selector);
        }

        public static IEnumerable<TResult> EachTrio<TSource, TResult>(this IEnumerable<TSource> e, Func<TSource, TSource, TSource, TResult> selector)
        {
            using (var it = e.GetEnumerator())
            {
                while (it.MoveNext())
                {
                    var a = it.Current;
                    TSource b = default, c = default;
                    if (it.MoveNext())
                    {
                        b = it.Current;
                        if (it.MoveNext())
                        {
                            c = it.Current;
                        }
                    }
                    yield return selector(a, b, c);
                }
            }
        }
    }
}