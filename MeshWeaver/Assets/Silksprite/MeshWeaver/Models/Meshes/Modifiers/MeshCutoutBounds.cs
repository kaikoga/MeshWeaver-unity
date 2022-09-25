using UnityEngine;

namespace Silksprite.MeshWeaver.Models.Meshes.Modifiers
{
    public class MeshCutoutBounds : MeshCutout
    {
        public MeshCutoutBounds(Bounds bounds, bool inside, int numVertex) : base(v => bounds.Contains(v), inside, numVertex) { }
    }
}