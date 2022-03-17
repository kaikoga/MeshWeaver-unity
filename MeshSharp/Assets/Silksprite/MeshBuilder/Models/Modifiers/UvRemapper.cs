using Silksprite.MeshBuilder.Extensions;
using Silksprite.MeshBuilder.Models.Base;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models.Modifiers
{
    public class UvRemapper : VertiesModifierBase
    {
        readonly Vector2 _min;
        readonly Vector2 _max;

        public UvRemapper(Vector2 min, Vector2 max)
        {
            _min = min;
            _max = max;
        }

        protected override Vertie Modify(Vertie vertie)
        {
            return vertie.WithUvs(vertie.Uvs.SelectMuxValues(uv => _min * (Vector2.one - uv) + (_max - _min) * uv));
        }
    }
}