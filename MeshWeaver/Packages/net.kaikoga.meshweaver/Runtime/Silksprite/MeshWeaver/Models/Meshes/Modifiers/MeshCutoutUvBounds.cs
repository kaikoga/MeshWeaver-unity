using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models.Meshes.Modifiers
{
    public class MeshCutoutUvBounds : MeshCutoutUv
    {
        [PublicAPI]
        public MeshCutoutUvBounds(Rect uvArea, int uvChannel, bool inside, int numVertex) : base(Predicate(uvArea), uvChannel, inside, numVertex) { }

        static Func<Vector2, bool> Predicate(Rect uvArea)
        {
            return uv => uvArea.Contains(uv);
        }

        [PublicAPI]
        public MeshCutoutUvBounds(IEnumerable<Rect> uvAreas, int uvChannel, bool inside, int numVertex) : base(Predicate(uvAreas), uvChannel, inside, numVertex) { }

        static Func<Vector2, bool> Predicate(IEnumerable<Rect> uvAreas)
        {
            var uvAreasArray = uvAreas.ToArray();
            return uv => uvAreasArray.Any(uvArea => uvArea.Contains(uv));
        }
    }
}