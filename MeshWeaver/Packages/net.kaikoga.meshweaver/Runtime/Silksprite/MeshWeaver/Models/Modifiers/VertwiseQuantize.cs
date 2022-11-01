using Silksprite.MeshWeaver.Models.Extensions;
using Silksprite.MeshWeaver.Models.Modifiers.Base;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models.Modifiers
{
    public class VertwiseQuantize : VertwiseModifierBase
    {
        protected override bool ValidateTriangles => true;

        readonly int _denominator;

        public VertwiseQuantize(int denominator)
        {
            _denominator = denominator;
        }

        protected override Vertie Modify(Vertie vertie)
        {
            var t = vertie.Translation;
            var pos = t.GetPosition();
            t = t.WithPosition(new Vector3(
                Mathf.Round(pos.x * _denominator) / _denominator,
                Mathf.Round(pos.y * _denominator) / _denominator,
                Mathf.Round(pos.z * _denominator) / _denominator));
            return vertie.WithTranslation(t);
        }
    }
}