using Silksprite.MeshWeaver.Models.Extensions;
using Silksprite.MeshWeaver.Models.Modifiers.Base;
using UnityEngine;

namespace Silksprite.MeshWeaver.Models.Modifiers
{
    public class VertwiseTranslate : VertwiseModifierBase
    {
        readonly Matrix4x4 _translation;

        public VertwiseTranslate(Matrix4x4 translation)
        {
            _translation = translation;
        }

        protected override Vertie Modify(Vertie vertie)
        {
            return vertie.WithTranslation(_translation * vertie.Translation);
        }
    }
}