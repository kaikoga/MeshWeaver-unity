using System;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models.DataObjects
{
    [Serializable]
    public class VertieData
    {
        public Matrix4x4 translation;
        public bool culled;
        public Vector2 uv;

        public static VertieData FromVertie(Vertie vertie)
        {
            return new VertieData
            {
                translation = vertie.Translation,
                culled = vertie.Culled,
                uv = vertie.Uv
            };
        }

        public Vertie ToVertie()
        {
            return new Vertie(translation, culled, uv);
        }
    }
}