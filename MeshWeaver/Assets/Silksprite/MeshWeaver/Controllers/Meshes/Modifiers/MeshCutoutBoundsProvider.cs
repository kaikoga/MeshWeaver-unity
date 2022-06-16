using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.CustomDrawers;
using Silksprite.MeshWeaver.Models.Meshes.Modifiers;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Meshes.Modifiers
{
    public class MeshCutoutBoundsProvider : MeshModifierProviderBase
    {
        [BoundsCustom]
        public Bounds bounds;
        public bool inside;
        [Range(0, 3)]
        public int numVertex = 1;

        public override IMeshieModifier MeshieModifier => new MeshCutoutBounds(bounds, inside, numVertex);
    }
}