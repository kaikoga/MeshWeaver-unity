namespace Silksprite.MeshBuilder.Models
{
    public readonly struct Channel<T>
    {
        public readonly T Value;
        public readonly int MinIndex;

        public Channel(T value, int minIndex)
        {
            Value = value;
            MinIndex = minIndex;
        }
    }
}