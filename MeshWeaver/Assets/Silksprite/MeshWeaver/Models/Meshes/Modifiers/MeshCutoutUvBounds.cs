using UnityEngine;

namespace Silksprite.MeshWeaver.Models.Meshes.Modifiers
{
    public class MeshCutoutUvBounds : MeshCutoutUv
    {
        public MeshCutoutUvBounds(Rect uvArea, int uvChannel, bool inside, int numVertex) : base(uv => uvArea.Contains(uv), uvChannel, inside, numVertex) { }
    }
}