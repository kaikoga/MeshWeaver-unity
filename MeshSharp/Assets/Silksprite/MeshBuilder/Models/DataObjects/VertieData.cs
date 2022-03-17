using System;
using System.Linq;
using Silksprite.MeshBuilder.Models.DataObjects.Extensions;
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
                uvs = Vector2MuxData.FromMux(vertie.Uvs)
            };
        }

        public Vertie ToVertie()
        {
            return new Vertie(translation, culled, uvs.ToMux());
        }
    }
}