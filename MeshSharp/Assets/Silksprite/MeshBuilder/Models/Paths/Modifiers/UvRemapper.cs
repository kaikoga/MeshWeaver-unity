using Silksprite.MeshBuilder.Extensions;
using Silksprite.MeshBuilder.Models.Base;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models.Paths.Modifiers
{
    public class UvRemapper : IPathieModifier
    {
        readonly Vector2 _min;
        readonly Vector2 _max;

        public UvRemapper(Vector2 min, Vector2 max)
        {
            _min = min;
            _max = max;
        }

        public Pathie Modify(Pathie pathie)
        {
            return pathie.Modify((vertie, i) => vertie.WithUvs(vertie.Uvs.SelectChannelValues(uv => _min * (Vector2.one - uv) + (_max - _min) * uv)));
        }
    }
}