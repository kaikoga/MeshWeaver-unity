using Silksprite.MeshBuilder.Controllers.Base.Modifiers;
using Silksprite.MeshBuilder.Models.Meshes.Modifiers;

namespace Silksprite.MeshBuilder.Controllers.Meshes.Modifiers
{
    public class MeshSetMaterialIndexProvider : MeshModifierProviderBase
    {
        public int materialIndex;

        public override IMeshieModifier MeshieModifier => new MeshSetMaterialIndex(materialIndex);
    }
}