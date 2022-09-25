using System;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models.Meshes.Modifiers
{
    public class MeshCutoutUvBounds : MeshCutoutUv
    {
        public MeshCutoutUvBounds(Rect uvArea, int uvChannel, bool inside, int numVertex) : base(Predicate(uvArea), uvChannel, inside, numVertex) { }

        static Func<Vector2, bool> Predicate(Rect uvArea)
        {
            return uv => uvArea.Contains(uv);
        }
    }
}