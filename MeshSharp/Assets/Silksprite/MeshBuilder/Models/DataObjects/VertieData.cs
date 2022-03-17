using System;
using System.Linq;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models.DataObjects
{
    [Serializable]
    public class VertieData
    {
        public Matrix4x4 translation;
        public bool culled;
        public Vector2MuxData[] uvs;

        public static VertieData FromVertie(Vertie vertie)
        {
            return new VertieData
            {
                translation = vertie.Translation,
                culled = vertie.Culled,
                uvs = vertie.Uvs.Select(Vector2MuxData.FromMuxLayer).ToArray()
            };
        }

        public Vertie ToVertie()
        {
            return new Vertie(translation, culled, uvs.Select(uv => uv.ToMuxLayer()));
        }
    }
}