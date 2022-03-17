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
        public Vector2ChannelData[] uvs;

        public static VertieData FromVertie(Vertie vertie)
        {
            return new VertieData
            {
                translation = vertie.Translation,
                culled = vertie.Culled,
                uvs = vertie.Uvs.Select(Vector2ChannelData.FromChannel).ToArray()
            };
        }

        public Vertie ToVertie()
        {
            return new Vertie(translation, culled, uvs.Select(uv => uv.ToChannel()));
        }
    }
}