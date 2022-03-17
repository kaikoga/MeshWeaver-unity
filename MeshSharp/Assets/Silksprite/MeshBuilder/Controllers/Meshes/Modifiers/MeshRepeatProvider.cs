using Silksprite.MeshBuilder.Controllers.Base.Modifiers;
using Silksprite.MeshBuilder.Controllers.Paths;
using Silksprite.MeshBuilder.Models.Base;
using Silksprite.MeshBuilder.Models.Meshes.Modifiers;
using UnityEngine;

namespace Silksprite.MeshBuilder.Controllers.Meshes.Modifiers
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