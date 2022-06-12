using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Models.Meshes.Modifiers;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Meshes.Modifiers
{
    public class MeshCutoutUvBoundsProvider : MeshModifierProviderBase
    {
        public Rect uvArea = new Rect(0f, 0f, 1f, 1f);
        public int uvChannel;
        public bool inside;
        [Range(0, 3)]
        public int numVertex = 1;

        public override IMeshieModifier MeshieModifier => new MeshCutoutUvBounds(uvArea, uvChannel, inside, numVertex);
    }
}