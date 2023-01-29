using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Controllers.Core;
using Silksprite.MeshWeaver.CustomDrawers;
using Silksprite.MeshWeaver.Models.Modifiers;
using Silksprite.MeshWeaver.Models.Modifiers.Base;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Modifiers
{
    public class UvProjectorProvider : VertwiseModifierProviderBase
    {
        public UvProjector.ProjectionKind projection;

        public Transform referenceTranslation;
        readonly TranslationCollector _referenceTranslationCollector = new TranslationCollector();
        
        public UvProjector.ProjectionAxisKind axisX = UvProjector.ProjectionAxisKind.XPlus;
        public UvProjector.ProjectionAxisKind axisY = UvProjector.ProjectionAxisKind.YPlus;
        [RectCustom]
        public Rect uvArea = new Rect(0, 0, 1f, 1f);
        public int uvChannel;

        protected override void Sync() => _referenceTranslationCollector.Sync(referenceTranslation);

        protected override VertwiseModifierBase CreateModifier()
        {
            var translation = _referenceTranslationCollector.Translate(Matrix4x4.identity);
            return new UvProjector(projection, translation, axisX, axisY, uvArea, uvChannel);
        }
    }
}