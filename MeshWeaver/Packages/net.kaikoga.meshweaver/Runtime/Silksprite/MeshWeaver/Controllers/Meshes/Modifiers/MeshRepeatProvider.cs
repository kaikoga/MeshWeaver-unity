using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Controllers.Core;
using Silksprite.MeshWeaver.Models.Meshes.Modifiers;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Meshes.Modifiers
{
    public class MeshRepeatProvider : MeshModifierProviderBase
    {
        public int count = 2;
        public Vector3 offset;

        public Transform offsetByReference;
        readonly TranslationCollector _offsetByReferenceCollector = new TranslationCollector();

        protected override void Sync() => _offsetByReferenceCollector.Sync(offsetByReference);

        protected override IMeshieModifier CreateModifier()
        {
            var translation = _offsetByReferenceCollector.Translate(Matrix4x4.Translate(offset));
            return new MeshRepeat(count, translation);
        }
    }
}