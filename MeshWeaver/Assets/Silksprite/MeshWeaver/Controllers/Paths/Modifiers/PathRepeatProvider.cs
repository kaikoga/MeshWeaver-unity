using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Models.Paths.Modifiers;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Paths.Modifiers
{
    public class PathRepeatProvider : PathModifierProviderBase
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

        public override IPathieModifier PathieModifier => new PathRepeat(count, Translation, fromPath);
    }
}