using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Controllers.Extensions;
using Silksprite.MeshWeaver.Models.Meshes.Modifiers;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Meshes.Modifiers
{
    public class MeshRepeatProvider : MeshModifierProviderBase
    {
        public int count = 2;
        public Vector3 offset;
        public Transform offsetByReference;
       
        Matrix4x4 Translation
        {
            get
            {
                var translation = Matrix4x4.Translate(offset);
                if (offsetByReference) translation = offsetByReference.ToLocalTranslation() * translation;
                return translation;
            }
        }

        protected override IMeshieModifier CreateModifier() => new MeshRepeat(count, Translation);

        protected override void RefreshUnityReferences() => AddUnityReference(offsetByReference);
    }
}