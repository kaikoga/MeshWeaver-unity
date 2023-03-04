using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Controllers.Core;
using Silksprite.MeshWeaver.Models.Meshes.Modifiers;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Meshes.Modifiers
{
    public class MeshExtrudeProvider : MeshModifierProviderBase
    {
        public bool fillBody = true;
        public bool fillBottom = true;
        public bool fillTop = true;
        public bool reverseLids;

        public bool isSmooth;

        public bool isFromCenter;
        public Vector3 offset = Vector3.right;

        public Transform offsetByReference;
        readonly TranslationCollector _offsetByReferenceCollector = new TranslationCollector();

        protected override int Sync() => _offsetByReferenceCollector.Sync(offsetByReference);

        protected override IMeshieModifier CreateModifier()
        {
            var translation = _offsetByReferenceCollector.Translate(Matrix4x4.Translate(offset));
            return new MeshExtrude(fillBody, fillBottom, fillTop, reverseLids, isSmooth, isFromCenter, translation);
        }
    }
}