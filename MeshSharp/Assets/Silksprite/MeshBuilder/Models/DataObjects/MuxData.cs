using System;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models.DataObjects
{
    [Serializable]
    public class MuxData<T>
    {
        public T value;
        public int minIndex;
        
        public MuxLayer<T> ToMuxLayer()
        {
            return new MuxLayer<T>(value, minIndex);
        }
    }

    [Serializable]
    public class Vector2MuxData : MuxData<Vector2>
    {
        public static Vector2MuxData FromMuxLayer(MuxLayer<Vector2> muxLayer)
        {
            return new Vector2MuxData
            {
                value = muxLayer.Value,
                minIndex = muxLayer.MinIndex
            };
        }
    }
}