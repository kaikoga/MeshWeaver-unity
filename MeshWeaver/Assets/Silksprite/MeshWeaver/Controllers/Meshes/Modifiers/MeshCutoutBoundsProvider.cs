using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Models.Meshes.Modifiers;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Meshes.Modifiers
{
    public class MeshCutoutBoundsProvider : MeshModifierProviderBase
    {
        public Bounds bounds;
        public bool inside;
        [Range(0, 3)]
        public int numVertex;

        public override IMeshieModifier MeshieModifier => new MeshCutoutBounds(bounds, inside, numVertex);
    }
}