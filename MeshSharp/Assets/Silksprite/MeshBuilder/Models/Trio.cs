namespace Silksprite.MeshBuilder.Models
{
    public readonly struct Trio<T>
        where T : struct
    {
        public readonly T Prev;
        public readonly T Self;
        public readonly T Next;

        public Trio(T prev, T self, T next)
        {
            Prev = prev;
            Self = self;
            Next = next;
        }
    }
}