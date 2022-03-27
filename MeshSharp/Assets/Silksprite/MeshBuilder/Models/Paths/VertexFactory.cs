namespace Silksprite.MeshBuilder.Models.Paths
{
    public class VertexFactory : IPathieFactory
    {
        readonly Vertie _vertex;

        public VertexFactory(Vertie vertex) => _vertex = vertex;

        public Pathie Build() => new Pathie(_vertex);
    }
}