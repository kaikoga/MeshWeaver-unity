namespace Silksprite.MeshWeaver.Models.Paths
{
    public interface IPathieFactory
    {
        Pathie Build(LodMaskLayer lod);
    }
}