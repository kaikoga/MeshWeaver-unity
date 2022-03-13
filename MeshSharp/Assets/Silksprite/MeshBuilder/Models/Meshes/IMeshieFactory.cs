namespace Silksprite.MeshBuilder.Models.Meshes
{
    public interface IMeshieFactory
    {
        Meshie Build();
    }
    
    public interface IMeshieFactory<in T>
    {
        Meshie Build(T arg);
    }
    
    public interface IMeshieFactory<in T1, in T2>
    {
        Meshie Build(T1 arg1, T2 arg2);
    }
}