namespace Silksprite.MeshWeaver.Models.Meshes
{
    public static class MeshieFactory
    {
        public static readonly IMeshieFactory Empty = new EmptyMeshieFactory();

        class EmptyMeshieFactory : IMeshieFactory
        {
            public Meshie Build(LodMaskLayer lod) => Meshie.Empty();
            public Pathie Extract(string pathName, LodMaskLayer lod) => Pathie.Empty();
        }
    }
}