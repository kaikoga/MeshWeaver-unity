using System.Collections.Generic;
using System.Linq;

namespace Silksprite.MeshBuilder.Models.DataObjects.Extensions
{
    public static class MuxDataExtension
    {
        public static Mux<T> ToMux<T>(this IEnumerable<MuxData<T>> self)
        {
            return new Mux<T>(self.Select(data => data.ToMuxLayer()));
        }
    }
}