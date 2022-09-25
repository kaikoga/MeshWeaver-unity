using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models.Meshes.Modifiers
{
    public class MeshCutoutBounds : MeshCutout
    {
        [PublicAPI]
        public MeshCutoutBounds(Bounds bounds, bool inside, int numVertex) : base(Predicate(bounds), inside, numVertex) { }

        static Func<Vector3, bool> Predicate(Bounds bounds)
        {
            return v => bounds.Contains(v);
        }

        [PublicAPI]
        public MeshCutoutBounds(IEnumerable<Bounds> bounds, bool inside, int numVertex) : base(Predicate(bounds), inside, numVertex) { }

        static Func<Vector3, bool> Predicate(IEnumerable<Bounds> bounds)
        {
            var boundsArray = bounds.ToArray();
            return v => boundsArray.Any(b => b.Contains(v));
        }
    }
}