using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Controllers.Paths;
using Silksprite.MeshWeaver.Models.Modifiers;
using Silksprite.MeshWeaver.Models.Modifiers.Base;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Modifiers
{
    public class UvProjectorProvider : VertwiseModifierProviderBase
    {
        public VertexProvider referenceTranslation;
        public int uvChannel;

        Matrix4x4 Translation
        {
            get
            {
                var translation = Matrix4x4.identity;
                if (referenceTranslation) translation = referenceTranslation.Translation;
                return translation;
            }
        }

        protected override VertwiseModifierBase VertwiseModifier => new UvProjector(Translation, uvChannel);
    }
}