using Silksprite.MeshBuilder.Models.Extensions;
using Silksprite.MeshBuilder.Models.Modifiers.Base;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models.Modifiers
{
    public class UvChannelRemapper : VertwiseModifierBase
    {
        readonly Vector2 _min;
        readonly Vector2 _max;
        readonly int _uvChannel;

        public UvChannelRemapper(Vector2 min, Vector2 max, int uvChannel)
        {
            _min = min;
            _max = max;
            _uvChannel = uvChannel;
        }

        protected override Vertie Modify(Vertie vertie)
        {
            return vertie.WithUvs(vertie.Uvs.SelectMux(
                (uv, ch) => ch != _uvChannel ? uv : _min * (Vector2.one - uv) + (_max - _min) * uv,
                (uv, ch) => ch));
        }
    }
}