using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Controllers.Extensions;
using Silksprite.MeshWeaver.Models.Modifiers;
using Silksprite.MeshWeaver.Models.Modifiers.Base;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Modifiers
{
    public class UvProjectorProvider : VertwiseModifierProviderBase
    {
        public UvProjector.ProjectionKind projection;
        public Transform referenceTranslation;
        public UvProjector.ProjectionAxisKind projectionAxis;
        public int uvChannel;

        Matrix4x4 Translation
        {
            get
            {
                var translation = Matrix4x4.identity;
                if (referenceTranslation) translation = referenceTranslation.ToLocalMatrix();
                return translation;
            }
        }

        protected override VertwiseModifierBase VertwiseModifier => new UvProjector(projection, Translation, projectionAxis, uvChannel);
    }
}