using Silksprite.MeshWeaver.Models.Extensions;
using Silksprite.MeshWeaver.Models.Modifiers.Base;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models.Modifiers
{
    public class UvRemapper : VertwiseModifierBase
    {
        readonly Rect _uvArea;

        public UvRemapper(Rect uvArea)
        {
            _uvArea = uvArea;
        }

        protected override Vertie Modify(Vertie vertie)
        {
            return vertie.WithUvs(vertie.Uvs.SelectMuxValues(uv => _uvArea.min * (Vector2.one - uv) + _uvArea.size * uv));
        }
    }
}