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

        protected override bool Sync() => _referenceTranslationCollector.Sync(referenceTranslation);

        protected override VertwiseModifierBase CreateModifier()
        {
            var translation = _referenceTranslationCollector.Translate(Matrix4x4.Scale(size));
            return new VertwiseTranslate(translation);
        }
    }
}