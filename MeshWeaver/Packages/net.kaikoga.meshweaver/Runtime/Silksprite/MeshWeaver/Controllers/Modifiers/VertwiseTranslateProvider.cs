using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Controllers.Extensions;
using Silksprite.MeshWeaver.Models.Modifiers;
using Silksprite.MeshWeaver.Models.Modifiers.Base;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Modifiers
{
    public class VertwiseTranslateProvider : VertwiseModifierProviderBase
    {
        // FIXME: Scale?
        public Vector3 size = Vector3.one;
        public Transform referenceTranslation;

        Matrix4x4 Translation
        {
            get
            {
                var translation = Matrix4x4.Scale(size);
                if (referenceTranslation) translation = referenceTranslation.ToLocalTranslation();
                return translation;
            }
        }

        protected override VertwiseModifierBase CreateModifier() => new VertwiseTranslate(Translation);
        
        protected override void RefreshUnityReferences() => AddUnityReference(referenceTranslation);
    }
}