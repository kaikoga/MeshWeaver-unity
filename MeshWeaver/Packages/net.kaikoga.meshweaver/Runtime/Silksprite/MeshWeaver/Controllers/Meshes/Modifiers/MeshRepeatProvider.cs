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

        Matrix4x4 Translation => _offsetByReferenceCollector.Translate(Matrix4x4.Translate(offset), offsetByReference);

        protected override IMeshieModifier CreateModifier() => new MeshRepeat(count, Translation);
    }
}