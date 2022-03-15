using Silksprite.MeshBuilder.Controllers.Base.Modifiers;
using Silksprite.MeshBuilder.Controllers.Paths;
using Silksprite.MeshBuilder.Models.Base;
using Silksprite.MeshBuilder.Models.Meshes.Modifiers;
using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers.Meshes.Modifiers
{
    public class UvProjectorProvider : MeshModifierProvider
    {
        public VertexProvider referenceTranslation;

        Matrix4x4 Translation
        {
            get
            {
                var translation = Matrix4x4.identity;
                if (referenceTranslation) translation = referenceTranslation.Translation;
                return translation;
            }
        }

        public override MeshieModifier Modifier => new UvProjector(Translation);
    }
}