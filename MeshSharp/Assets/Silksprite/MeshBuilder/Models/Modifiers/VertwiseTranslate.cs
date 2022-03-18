using Silksprite.MeshBuilder.Extensions;
using Silksprite.MeshBuilder.Models.Base;
using UnityEngine;

namespace Silksprite.MeshBuilder.Models.Modifiers
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