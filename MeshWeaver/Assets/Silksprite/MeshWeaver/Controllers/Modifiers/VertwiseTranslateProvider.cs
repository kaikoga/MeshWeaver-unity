using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Controllers.Paths;
using Silksprite.MeshWeaver.Models.Modifiers;
using Silksprite.MeshWeaver.Models.Modifiers.Base;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Modifiers
{
    public class VertwiseTranslateProvider : VertwiseModifierProviderBase
    {
        public Vector3 size = Vector3.one;
        public VertexProvider referenceTranslation;

        Matrix4x4 Translation
        {
            get
            {
                var translation = Matrix4x4.Scale(size);
                if (referenceTranslation) translation = referenceTranslation.Translation;
                return translation;
            }
        }

        protected override VertwiseModifierBase VertwiseModifier => new VertwiseTranslate(Translation);
    }
}