using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Models.Meshes.Modifiers;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Meshes.Modifiers
{
    public class MeshCutoutUvBoundsProvider : MeshModifierProviderBase
    {
        public Vector2 min;
        public Vector2 max;
        public int uvChannel;
        public bool inside;
        [Range(0, 3)]
        public int numVertex = 1;

        public override IMeshieModifier MeshieModifier => new MeshCutoutUvBounds(min, max, uvChannel, inside, numVertex);
    }
}