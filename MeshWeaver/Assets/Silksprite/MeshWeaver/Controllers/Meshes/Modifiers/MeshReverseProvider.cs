using Silksprite.MeshWeaver.Controllers.Paths;
using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Models.Meshes.Modifiers;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Meshes.Modifiers
{
    public class MeshReverseProvider : MeshModifierProviderBase
    {
        public bool front = true;
        public bool back = false;

        public override IMeshieModifier MeshieModifier => new MeshReverse(front, back);
    }
}