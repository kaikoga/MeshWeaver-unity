using System.Linq;
using Silksprite.MeshWeaver.Models.Extensions;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models.Paths.Modifiers
{
    public class UvGenerator : IPathieModifier
    {
        readonly Vector2 _min;
        readonly Vector2 _max;

        readonly float _topologicalWeight;
        readonly int _uvChannel;

        public UvGenerator(Vector2 min, Vector2 max, float topologicalWeight, int uvChannel)
        {
            _min = min;
            _max = max;
            _topologicalWeight = topologicalWeight;
            _uvChannel = uvChannel;
        }

        public Pathie Modify(Pathie pathie)
        {
            var iMax = pathie.Vertices.Count - 1;
            if (_topologicalWeight == 0)
            {
                return pathie.Modify((vertie, i) => vertie.AddUv(_min + (_max - _min) * i / iMax, _uvChannel));
            }

            var lengths = new [] { 0f }.Concat(pathie.Vertices.Pairwise((a, b) => (b.Vertex - a.Vertex).magnitude).Integral()).ToArray();
            var lMax = lengths[lengths.Length - 1];

            var vertices = pathie.Vertices.Select((vertie, i) =>
            {
                var ordinal = (float)i / iMax;
                var topological = lengths[i] / lMax;
                var t = ordinal * (1 - _topologicalWeight) + topological * _topologicalWeight;
                return vertie.AddUv(_min + (_max - _min) * t, _uvChannel);
            });
            return new Pathie(vertices);
        }
    }
}