using System;
using System.Linq;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models.DataObjects
{
    [Serializable]
    public class MuxData<T>
    {
        public T value;
        public int channel;
        
        public MuxLayer<T> ToMuxLayer()
        {
            return new MuxLayer<T>(value, channel);
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
                channel = muxLayer.Channel
            };
        }
        
        public static Vector2MuxData[] FromMux(Mux<Vector2> mux) => mux.Select(FromMuxLayer).ToArray();
    }
}