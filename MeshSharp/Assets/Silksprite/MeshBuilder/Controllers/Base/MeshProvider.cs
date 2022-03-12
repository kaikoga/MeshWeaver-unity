using System.Linq;
using Silksprite.MeshBuilder.Controllers.Base.Modifiers;
using Silksprite.MeshBuilder.Models;

namespace Silksprite.MeshBuilder.Controllers.Base
{
    public abstract class MeshProvider : GeometryProvider
    {
        public LodMask lodMask = LodMask.All;

        public Meshie LastMeshie { get; private set; }

        public Meshie ToMeshie(LodMask lod)
        {
            var meshie = GenerateMeshie(lod);
            LastMeshie = GetComponents<MeshModifierProvider>()
                .Where(provider => provider.enabled)
                .Select(provider => provider.Modifier)
                .Aggregate(meshie, (current, modifier) => modifier.Modify(current));
            return LastMeshie;
        }

        protected abstract Meshie GenerateMeshie(LodMask lod);
    }
}