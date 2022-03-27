namespace Silksprite.MeshBuilder.Models.Meshes
{
    public class EmptyMeshieFactory : IMeshieFactory
    {
        public Meshie Build(LodMaskLayer lod) => Meshie.Empty();
    }
}