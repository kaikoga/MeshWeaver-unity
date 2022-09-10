using System.Collections.Generic;
using System.Linq;
using Silksprite.MeshWeaver.Models.Extensions;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models.Paths.Modifiers
{
    public class PathBevel : IPathieModifier
    {
        readonly float _subdivision;
        readonly float _size;
        readonly float _strength;

        public PathBevel(int subdivision, float size, float strength)
        {
            _subdivision = subdivision;
            _size = size;
            _strength = strength;
        }

        public Pathie Modify(Pathie pathie)
        {
            var vertices = pathie.Vertices;
            if (vertices.Count < 2) return pathie;
            var workVertices = vertices.ToArray();

            var halfSize = _size / 2;

            Vertie Curve(Vertie a, Vertie b, Vertie c, float t)
            {
                var sharpness = _strength < 0f ? -_strength : 0f;
                var round = _strength < 0f ? 1f + _strength : 1f - _strength;
                var bevel = _strength < 0f ? 0f : _strength;

                var roundA = Mathf.Sqrt(0.5f);
                var roundB = 1 - Mathf.Sqrt(0.5f);

                var wa = halfSize * (t * sharpness + (roundA * t + roundB) * round + (0.5f * t + 0.5f) * bevel);
                var wc = halfSize * (0 * sharpness + (1f - Mathf.Sqrt(1f - roundA * (1f - t) * roundA * (1f - t))) * round + (-0.5f * t + 0.5f) * bevel);
                var wb = 1 - wa - wc;

                return a * wa + b * wb + c * wc;
            }

            IEnumerable<Vertie> Bevel(Vertie a, Vertie b, Vertie c)
            {
                yield return Curve(a, b, c, 1f);
                for (var i = 1; i < _subdivision; i++)
                {
                    var t = i * 2f / _subdivision - 1f;
                    if (t < 0f)
                    {
                        yield return Curve(a, b, c, -t);
                    }
                    else
                    {
                        yield return Curve(c, b, a, t);
                    }
                }
                yield return Curve(c, b, a, 1f);
            }

            var builder = Pathie.Builder(pathie.isLoop);
            builder.Vertices.Add(workVertices[0]);
            builder.Vertices.AddRange(workVertices.EachSlidingTrio(Bevel).SelectMany(v => v));
            builder.Vertices.Add(workVertices[workVertices.Length - 1]);
            return builder.ToPathie();
        }
    }
}