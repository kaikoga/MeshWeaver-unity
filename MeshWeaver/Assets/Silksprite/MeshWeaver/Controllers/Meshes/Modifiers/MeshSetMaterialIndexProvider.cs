using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Models.Meshes.Modifiers;

namespace Silksprite.MeshWeaver.Controllers.Meshes.Modifiers
{
    public class MeshSetMaterialIndexProvider : MeshModifierProviderBase
    {
        public int materialIndex;

        protected override IMeshieModifier CreateModifier() => new MeshSetMaterialIndex(materialIndex);
    }
}