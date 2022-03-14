using System;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models.DataObjects
{
    [Serializable]
    public class VertieData
    {
        public Matrix4x4 translation;
        public Vector2 uv;
        public bool culled;

        public static VertieData FromVertie(Vertie vertie)
        {
            return new VertieData
            {
                translation = vertie.Translation,
                uv = vertie.Uv,
                culled = vertie.Culled
            };
        }

        public Vertie ToVertie()
        {
            return new Vertie(translation, uv, culled);
        }
    }
}