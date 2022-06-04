using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Silksprite.MeshWeaver.Utils
{
    public static class BoundsUtil
    {
        public static Bounds CalculateBounds(IEnumerable<Vector3> points)
        {
            var bounds = new Bounds(points.FirstOrDefault(), Vector3.zero);
            foreach (var point in points)
            {
                bounds.Encapsulate(point);
            }

            var magnitude = bounds.extents.magnitude;
            if (magnitude < 2f) bounds.Expand(2f - bounds.extents.magnitude);
            return bounds;
        }
    }
}