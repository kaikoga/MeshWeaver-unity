using System;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models.DataObjects
{
    [Serializable]
    public class ChannelData<T>
    {
        public T value;
        public int minIndex;
        
        public Channel<T> ToChannel()
        {
            return new Channel<T>(value, minIndex);
        }
    }

    [Serializable]
    public class Vector2ChannelData : ChannelData<Vector2>
    {
        public static Vector2ChannelData FromChannel(Channel<Vector2> channel)
        {
            return new Vector2ChannelData
            {
                value = channel.Value,
                minIndex = channel.MinIndex
            };
        }
    }
}