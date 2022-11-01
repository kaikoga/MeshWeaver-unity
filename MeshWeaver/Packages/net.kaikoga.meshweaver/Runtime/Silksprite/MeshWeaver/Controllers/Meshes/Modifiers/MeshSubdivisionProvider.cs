using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Models.Meshes.Modifiers;

namespace Silksprite.MeshWeaver.Controllers.Meshes.Modifiers
{
    public class MeshSubdivisionProvider : MeshModifierProviderBase
    {
        public int count = 1;

        protected override IMeshieModifier CreateModifier() => new MeshSubdivision(count);
    }
}