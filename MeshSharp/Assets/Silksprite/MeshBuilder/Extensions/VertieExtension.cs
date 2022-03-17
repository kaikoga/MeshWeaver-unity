using Silksprite.MeshBuilder.Models;
using UnityEngine;

namespace Silksprite.MeshBuilder.Extensions
{
    public static class VertieExtension
    {
        public static Vertie WithTranslation(this Vertie v, Matrix4x4 translation)
        {
            return new Vertie(translation, v.Culled, v.Uvs);
        }

        public static Vertie WithUv(this Vertie v, Vector2 uv)
        {
            return new Vertie(v.Translation, v.Culled, new [] { new UvChannel(uv, 0) });
        }
    }
}