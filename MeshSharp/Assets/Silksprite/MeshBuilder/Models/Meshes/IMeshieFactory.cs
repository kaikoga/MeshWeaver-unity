namespace Silksprite.MeshBuilder.Models.Meshes
{
    public interface IMeshieFactory
    {
        Meshie Build(LodMaskLayer lod);
    }
}