using Silksprite.MeshBuilder.Controllers.Base.Modifiers;
using Silksprite.MeshBuilder.Controllers.Paths;
using Silksprite.MeshBuilder.Models.Base;
using Silksprite.MeshBuilder.Models.Meshes.Modifiers;
using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers.Meshes.Modifiers
{
    public class MeshResizeProvider : MeshModifierProvider
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

        public override MeshieModifier Modifier => new MeshResize(Translation);
    }
}