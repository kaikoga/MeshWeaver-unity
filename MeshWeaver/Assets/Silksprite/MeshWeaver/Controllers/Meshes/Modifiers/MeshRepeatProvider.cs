using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Controllers.Paths;
using Silksprite.MeshWeaver.Models.Meshes.Modifiers;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Meshes.Modifiers
{
    public class MeshRepeatProvider : MeshModifierProviderBase
    {
        public int count = 2;
        public Vector3 offset;
        public VertexProvider referenceTranslation;
       
        Matrix4x4 Translation
        {
            get
            {
                var translation = Matrix4x4.Translate(offset);
                if (referenceTranslation) translation = referenceTranslation.Translation * translation;
                return translation;
            }
        }

        public override IMeshieModifier MeshieModifier => new MeshRepeat(count, Translation);
    }
}