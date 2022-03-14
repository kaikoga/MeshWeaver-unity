using Silksprite.MeshBuilder.Models.Base;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models.Paths.Modifiers
{
    public class UvRemapper : PathieModifier
    {
        readonly Vector2 _min;
        readonly Vector2 _max;

        public UvRemapper(Vector2 min, Vector2 max)
        {
            _min = min;
            _max = max;
        }

        public override Pathie Modify(Pathie pathie)
        {
            return pathie.Modify((vertie, i) => new Vertie(vertie.Translation, _min * (Vector2.one - vertie.Uv) + (_max - _min) * vertie.Uv, vertie.Culled));
        }
    }
}