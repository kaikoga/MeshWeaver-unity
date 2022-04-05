using System.Linq;

namespace Silksprite.MeshWeaver.Models.Extensions
{
    public static class GonExtension
    {
        public static Gon WithMaterialIndex(this Gon self, int materialIndex) => new Gon(self.Indices.ToArray(), materialIndex);
    }
}