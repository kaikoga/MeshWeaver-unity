using Silksprite.MeshWeaver.Models.Extensions;
using Silksprite.MeshWeaver.Models.Modifiers.Base;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models.Modifiers
{
    public class UvChannelRemapper : VertwiseModifierBase
    {
        readonly Rect _uvArea;
        readonly int _uvChannel;

        public UvChannelRemapper(Rect uvArea, int uvChannel)
        {
            _uvArea = uvArea;
            _uvChannel = uvChannel;
        }

        protected override Vertie Modify(Vertie vertie)
        {
            return vertie.WithUvs(vertie.Uvs.WithMuxChannelValue(_uvChannel, uv => _uvArea.min * (Vector2.one - uv) + _uvArea.size * uv));
        }
    }
}