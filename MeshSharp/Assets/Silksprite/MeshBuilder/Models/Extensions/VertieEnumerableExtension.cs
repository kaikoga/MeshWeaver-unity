using System.Collections.Generic;
using System.Linq;

namespace Silksprite.MeshBuilder.Models.Extensions
{
    public static class VertieEnumerableExtensions
    {
        public static IEnumerable<Trio<Vertie>> SelectTrios(this IEnumerable<Vertie> enumerable)
        {
            var a = enumerable.ToArray();
            if (a.Length < 2) yield break;

            var lastIndex = a.Length - 1;
            yield return new Trio<Vertie>(a[0] + a[0] - a[1], a[0], a[1]);
            for (var i = 1; i < lastIndex; i++)
            {
                yield return new Trio<Vertie>(a[i - 1], a[i], a[i + 1]);
            }
            yield return new Trio<Vertie>(a[lastIndex - 1], a[lastIndex], a[lastIndex] + a[lastIndex] - a[lastIndex - 1]);
        }
    }
}