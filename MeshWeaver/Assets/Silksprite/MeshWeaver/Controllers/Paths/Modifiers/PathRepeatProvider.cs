using Silksprite.MeshWeaver.Controllers.Base.Modifiers;
using Silksprite.MeshWeaver.Controllers.Extensions;
using Silksprite.MeshWeaver.Models.Paths.Modifiers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Silksprite.MeshWeaver.Controllers.Paths.Modifiers
{
    public class PathRepeatProvider : PathModifierProviderBase
    {
        public int count = 2;
        public Vector3 offset;
        public Transform offsetByReference;
        public bool offsetByPath = true;
        public bool smoothJoin = false;

        Matrix4x4 Translation
        {
            get
            {
                var translation = Matrix4x4.Translate(offset);
                if (offsetByReference) translation = offsetByReference.ToLocalMatrix() * translation;
                return translation;
            }
        }

        protected override IPathieModifier CreateModifier() => new PathRepeat(count, Translation, offsetByPath, smoothJoin);
        
        protected override void RefreshUnityReferences() => AddUnityReference(offsetByReference);
    }
}