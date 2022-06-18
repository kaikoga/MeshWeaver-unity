using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Models.Meshes.Modifiers;

namespace Silksprite.MeshWeaver.Controllers.Meshes.Modifiers
{
    public class MeshReverseProvider : MeshModifierProviderBase
    {
        public bool front = true;
        public bool back = false;

        protected override IMeshieModifier CreateModifier() => new MeshReverse(front, back);
    }
}