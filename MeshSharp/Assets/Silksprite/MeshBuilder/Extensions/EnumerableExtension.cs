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
    }
}