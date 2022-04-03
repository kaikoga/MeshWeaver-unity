using Silksprite.MeshBuilder.Controllers.Base.Modifiers;
using Silksprite.MeshBuilder.Models.Meshes.Modifiers;

namespace Silksprite.MeshBuilder.Controllers.Meshes.Modifiers
{
    public class MeshSubdivisionProvider : MeshModifierProviderBase
    {
        public int count = 1;

        public override IMeshieModifier MeshieModifier => new MeshSubdivision(count);
    }
}