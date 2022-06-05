using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models.Utils
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

            return bounds;
        }
    }
}