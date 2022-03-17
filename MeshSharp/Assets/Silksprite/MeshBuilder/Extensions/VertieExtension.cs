using System.Collections.Generic;
using System.Linq;
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

        public static Vertie WithUvs(this Vertie v, IEnumerable<MuxLayer<Vector2>> uvs)
        {
            return new Vertie(v.Translation, v.Culled, uvs);
        }

        public static Vertie AddUv(this Vertie v, MuxLayer<Vector2> uv)
        {
            return new Vertie(v.Translation, v.Culled, v.Uvs.Concat(Enumerable.Repeat(uv, 1)));
        }

        public static Vertie WithUvIndex(this Vertie v, int uvIndex)
        {
            return new Vertie(v.Translation, v.Culled, v.Uvs.Select(uv => new MuxLayer<Vector2>(uv.Value, uv.MinIndex - uvIndex)));
        }
    }
}