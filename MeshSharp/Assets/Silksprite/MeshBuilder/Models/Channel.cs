using System;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models
{
    [Serializable]
    public class Channel<T>
    {
        public readonly T Value;
        public readonly int MinIndex;

        public Channel(T value, int minIndex)
        {
            Value = value;
            MinIndex = minIndex;
        }
    }

    [Serializable]
    public class UvChannel : Channel<Vector2>
    {
        public UvChannel(Vector2 value, int minIndex) : base(value, minIndex)
        {
        }
    }
}