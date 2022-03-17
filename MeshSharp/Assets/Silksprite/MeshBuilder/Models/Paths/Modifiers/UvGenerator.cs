using Silksprite.MeshBuilder.Extensions;
using Silksprite.MeshBuilder.Models.Base;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models.Paths.Modifiers
{
    public class UvGenerator : IPathieModifier
    {
        readonly Vector2 _min;
        readonly Vector2 _max;

        readonly int _uvChannel;

        public UvGenerator(Vector2 min, Vector2 max, int uvChannel)
        {
            _min = min;
            _max = max;
            _uvChannel = uvChannel;
        }

        public Pathie Modify(Pathie pathie)
        {
            var iMax = pathie.Vertices.Count - 1;
            return pathie.Modify((vertie, i) => vertie.AddUv(_min + (_max - _min) * i / iMax, _uvChannel));
        }
    }
}