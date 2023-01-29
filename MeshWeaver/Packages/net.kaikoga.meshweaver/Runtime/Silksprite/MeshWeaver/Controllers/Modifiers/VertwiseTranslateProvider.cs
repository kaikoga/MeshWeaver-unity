using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Controllers.Core;
using Silksprite.MeshWeaver.Models.Modifiers;
using Silksprite.MeshWeaver.Models.Modifiers.Base;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Modifiers
{
    public class VertwiseTranslateProvider : VertwiseModifierProviderBase
    {
        // FIXME: Scale?
        public Vector3 size = Vector3.one;

        public Transform referenceTranslation;
        readonly TranslationCollector _referenceTranslationCollector = new TranslationCollector();

        Matrix4x4 Translation => _referenceTranslationCollector.Translate(Matrix4x4.Scale(size), referenceTranslation);

        protected override VertwiseModifierBase CreateModifier() => new VertwiseTranslate(Translation);
    }
}