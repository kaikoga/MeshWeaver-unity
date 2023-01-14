using System.Linq;
using Silksprite.MeshWeaver.Models.Extensions;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models.Paths.Modifiers
{
    public class UvGenerator : IPathieModifier
    {
        readonly Rect _uvArea;

        readonly bool _absoluteScale;
        readonly float _topologicalWeight;
        readonly int _uvChannel;

        public UvGenerator(Rect uvArea, bool absoluteScale, float topologicalWeight, int uvChannel)
        {
            _uvArea = uvArea;
            _absoluteScale = absoluteScale;
            _topologicalWeight = topologicalWeight;
            _uvChannel = uvChannel;
        }

        public Pathie Modify(Pathie pathie)
        {
            if (pathie.isLoop)
            {
                var p = PathieExtension.CloseLoop(pathie);
                pathie = p;
            }

            var iMax = pathie.Vertices.Count - 1;
            if (_topologicalWeight == 0 && !_absoluteScale)
            {
                return pathie.Modify((vertie, i) => vertie.AddUv(_uvArea.min + _uvArea.size * ((float)i / iMax), _uvChannel));
            }

            var lengths = pathie.ToNetLengths().ToArray();
            var lMax = lengths[lengths.Length - 1];
            var scale = _absoluteScale ? lMax : 1;

            if (_topologicalWeight == 0 || lMax == 0f)
            {
                return pathie.Modify((vertie, i) => vertie.AddUv(_uvArea.min + _uvArea.size * (scale * i / iMax), _uvChannel));
            }

            var vertices = pathie.Vertices.Select((vertie, i) =>
            {
                var ordinal = (float)i / iMax;
                var topological = lengths[i] / lMax;
                var t = ordinal * (1 - _topologicalWeight) + topological * _topologicalWeight;
                return vertie.AddUv(_uvArea.min + _uvArea.size * (scale * t), _uvChannel);
            });
            return new Pathie(vertices, false);
        }
    }
}