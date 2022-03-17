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

        public static Vertie WithUvs(this Vertie v, Mux<Vector2> uvs)
        {
            return new Vertie(v.Translation, v.Culled, uvs);
        }

        public static Vertie AddUv(this Vertie v, Vector2 uv, int ch)
        {
            return new Vertie(v.Translation, v.Culled, v.Uvs.AddLayer(uv, ch));
        }

        public static Vertie WithUvCh(this Vertie v, int uvCh)
        {
            return new Vertie(v.Translation, v.Culled, v.Uvs.SelectMuxChannels(ch => ch - uvCh));
        }
    }
}