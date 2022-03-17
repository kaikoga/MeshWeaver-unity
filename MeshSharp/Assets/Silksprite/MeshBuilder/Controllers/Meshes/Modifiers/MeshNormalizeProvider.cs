using Silksprite.MeshBuilder.Controllers.Base.Modifiers;
using Silksprite.MeshBuilder.Controllers.Paths;
using Silksprite.MeshBuilder.Models.Base;
using Silksprite.MeshBuilder.Models.Meshes.Modifiers;

namespace Silksprite.MeshBuilder.Controllers.Meshes.Modifiers
{
    public class MeshNormalizeProvider : MeshModifierProviderBase
    {
        public override IMeshieModifier MeshieModifier => new MeshNormalize();
    }
}