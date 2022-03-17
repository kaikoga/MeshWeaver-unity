using Silksprite.MeshBuilder.Controllers.Base.Modifiers;
using Silksprite.MeshBuilder.Controllers.Paths;
using Silksprite.MeshBuilder.Models.Base;
using Silksprite.MeshBuilder.Models.Meshes.Modifiers;
using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers.Meshes.Modifiers
{
    public class MeshReverseProvider : MeshModifierProviderBase
    {
        public bool front = true;
        public bool back = false;

        public override IMeshieModifier MeshieModifier => new MeshReverse(front, back);
    }
}