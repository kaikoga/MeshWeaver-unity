using Silksprite.MeshBuilder.Controllers.Base.Modifiers;
using Silksprite.MeshBuilder.Controllers.Paths;
using Silksprite.MeshBuilder.Models.Base;
using Silksprite.MeshBuilder.Models.Modifiers;
using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers.Modifiers
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