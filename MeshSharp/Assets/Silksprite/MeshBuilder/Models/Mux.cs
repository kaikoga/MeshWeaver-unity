namespace Silksprite.MeshBuilder.Models
{
    public readonly struct MuxLayer<T>
    {
        public readonly T Value;
        public readonly int MinIndex;

        public MuxLayer(T value, int minIndex)
        {
            Value = value;
            MinIndex = minIndex;
        }
    }
}