using System.Linq;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models.Extensions
{
    public static class GonExtension
    {
        public static Gon WithMaterial(this Gon self, Material material) => new Gon(self.Indices.ToArray(), material);
    }
}