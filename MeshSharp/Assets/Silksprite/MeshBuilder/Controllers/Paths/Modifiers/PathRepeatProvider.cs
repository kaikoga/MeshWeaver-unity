using Silksprite.MeshBuilder.Controllers.Base.Modifiers;
using Silksprite.MeshBuilder.Models.Base;
using Silksprite.MeshBuilder.Models.Paths.Modifiers;
using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers.Paths.Modifiers
{
    public class PathRepeatProvider : PathModifierProvider
    {
        public int count = 2;
        public Vector3 offset;
        public VertexProvider referenceTranslation;
        public bool fromPath = true;

        Matrix4x4 Translation
        {
            get
            {
                var translation = Matrix4x4.Translate(offset);
                if (referenceTranslation) translation = referenceTranslation.Translation * translation;
                return translation;
            }
        }

        public override PathieModifier Modifier => new PathRepeat(count, Translation, fromPath);
    }
}