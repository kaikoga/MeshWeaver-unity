namespace Silksprite.MeshBuilder.Models.Paths
{
    public class EmptyPathieFactory : IPathieFactory
    {
        public Pathie Build(LodMaskLayer lod) => Pathie.Empty();
    }
}