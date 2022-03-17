using Silksprite.MeshBuilder.Controllers.Base.Modifiers;
using Silksprite.MeshBuilder.Models.Base;
using Silksprite.MeshBuilder.Models.Meshes.Modifiers;

namespace Silksprite.MeshBuilder.Controllers.Meshes.Modifiers
{
    public class MeshSubdivisionProvider : MeshModifierProvider
    {
        public int count = 1;

        public override IMeshieModifier MeshieModifier => new MeshSubdivision(count);
    }
}