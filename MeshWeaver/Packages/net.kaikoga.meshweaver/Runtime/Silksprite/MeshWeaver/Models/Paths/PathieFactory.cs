namespace Silksprite.MeshWeaver.Models.Paths
{
    public static class PathieFactory
    {
        public static readonly IPathieFactory Empty = new EmptyPathieFactory();

        class EmptyPathieFactory : IPathieFactory
        {
            public Pathie Build(LodMaskLayer lod) => Pathie.Empty();
        }
    }
}