using UnityEngine;

namespace Silksprite.MeshWeaver.Models.Extensions
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

        public static Vertie AddUv(this Vertie v, Vector2 uv, int channel)
        {
            return new Vertie(v.Translation, v.Culled, v.Uvs.AddLayer(uv, channel));
        }

        public static Vertie ShiftUvChannel(this Vertie v, int uvChannel)
        {
            return new Vertie(v.Translation, v.Culled, v.Uvs.Shift(uvChannel));
        }
    }
}