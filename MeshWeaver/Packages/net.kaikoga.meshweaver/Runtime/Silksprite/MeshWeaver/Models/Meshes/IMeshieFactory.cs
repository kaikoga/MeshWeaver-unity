namespace Silksprite.MeshWeaver.Models.Meshes
{
    public interface IMeshieFactory
    {
        Meshie Build(LodMaskLayer lod);
        Pathie Extract(string pathName, LodMaskLayer lod);
    }
}