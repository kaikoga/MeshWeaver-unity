using System;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models.Meshes.Modifiers
{
    public class MeshCutoutBounds : MeshCutout
    {
        public MeshCutoutBounds(Bounds bounds, bool inside, int numVertex) : base(Predicate(bounds), inside, numVertex) { }

        static Func<Vector3, bool> Predicate(Bounds bounds)
        {
            return v => bounds.Contains(v);
        }
    }
}