using System.Linq;
using Silksprite.MeshBuilder.Controllers.Base.Modifiers;
using Silksprite.MeshBuilder.Models;

namespace Silksprite.MeshBuilder.Controllers.Base
{
    public abstract class MeshProvider : GeometryProvider
    {
        public Meshie LastMeshie { get; private set; }

        public Meshie ToMeshie()
        {
            var meshie = GenerateMeshie();
            LastMeshie = GetComponents<MeshModifierProvider>()
                .Where(provider => provider.enabled)
                .Select(provider => provider.Modifier)
                .Aggregate(meshie, (current, modifier) => modifier.Modify(current));
            return LastMeshie;
        }

        protected abstract Meshie GenerateMeshie();
    }
}