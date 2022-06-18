using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Controllers.Paths;
using Silksprite.MeshWeaver.Models.Meshes.Modifiers;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Meshes.Modifiers
{
    public class MeshDisassembleProvider : MeshModifierProviderBase
    {
        protected override IMeshieModifier CreateModifier() => new MeshDisassemble();
    }
}