using System.Collections.Generic;
using System.Linq;

namespace Silksprite.MeshWeaver.Models.DataObjects.Extensions
{
    public static class MuxDataExtension
    {
        public static Mux<T> ToMux<T>(this IEnumerable<MuxData<T>> self)
        {
            if (self == null) return Mux.Empty<T>();
            return Mux.Build(self.Select(data => data.ToMuxLayer()));
        }
    }
}