using System.Collections.Generic;
using Silksprite.MeshWeaver.Models.Utils;
using UnityEngine;

namespace Silksprite.MeshWeaver.Utils
{
    public static class EditorBoundsUtil
    {
        public static Bounds CalculateFrameBounds(IEnumerable<Vector3> points)
        {
            var bounds = BoundsUtil.CalculateBounds(points);

            var magnitude = bounds.extents.magnitude;
            if (magnitude < 2f) bounds.Expand(2f - bounds.extents.magnitude);
            return bounds;
        }
    }
}