using System;
using System.Collections.Generic;
using System.Linq;

namespace Silksprite.MeshBuilder.Extensions
{
    public static class EnumerableExtension
    {
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