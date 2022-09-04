using System;
using System.Linq;
using Silksprite.MeshWeaver.Models.DataObjects.Extensions;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models.DataObjects
{
    [Serializable]
    public class VertieData
    {
        public Matrix4x4 translation;
        public Vector2MuxData[] uvs;

        public static VertieData FromVertie(Vertie vertie)
        {
            return new VertieData
            {
                translation = vertie.Translation,
                uvs = Vector2MuxData.FromMux(vertie.Uvs)
            };
        }

        public Vertie ToVertie()
        {
            return new Vertie(translation, uvs.ToMux());
        }
    }
}