using System.Linq;
using Silksprite.MeshBuilder.Models.Extensions;

namespace Silksprite.MeshBuilder.Models.Meshes.Modifiers
{
    public class MeshSetMaterialIndex : IMeshieModifier
    {
        readonly int _materialIndex;

        public MeshSetMaterialIndex(int materialIndex)
        {
            _materialIndex = materialIndex;
        }

        public Meshie Modify(Meshie meshie)
        {
            return new Meshie(meshie.Vertices, meshie.Gons.Select(gon => gon.WithMaterialIndex(_materialIndex)));
        }
    }
}