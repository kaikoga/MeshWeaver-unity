using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Controllers.Core;
using Silksprite.MeshWeaver.Models.Paths.Modifiers;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Paths.Modifiers
{
    public class PathRepeatProvider : PathModifierProviderBase
    {
        public int count = 2;
        public Vector3 offset;
        
        public Transform offsetByReference;
        readonly TranslationCollector _offsetByReferenceCollector = new TranslationCollector();

        public bool offsetByPath = true;
        public bool smoothJoin = false;

        Matrix4x4 Translation => _offsetByReferenceCollector.Translate(Matrix4x4.Translate(offset), offsetByReference);

        protected override IPathieModifier CreateModifier() => new PathRepeat(count, Translation, offsetByPath, smoothJoin);
    }
}