using Silksprite.MeshBuilder.Controllers.Base.Modifiers;
using Silksprite.MeshBuilder.Controllers.Paths;
using Silksprite.MeshBuilder.Models.Base;
using Silksprite.MeshBuilder.Models.Modifiers;
using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers.Modifiers
{
    public class VertwiseResizeProvider : VertwiseModifierProviderBase
    {
        public Vector3 size = Vector3.one;
        public VertexProvider referenceTranslation;

        Matrix4x4 Translation
        {
            get
            {
                var translation = Matrix4x4.Scale(size);
                if (referenceTranslation) translation = referenceTranslation.Translation;
                return translation;
            }
        }

        protected override VertwiseModifierBase VertwiseModifier => new MeshResize(Translation);
    }
}