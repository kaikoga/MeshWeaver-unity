using Silksprite.MeshWeaver.Controllers.Base;
using Silksprite.MeshWeaver.Controllers.Extensions;
using Silksprite.MeshWeaver.CustomDrawers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Silksprite.MeshWeaver.Controllers
{
    [DisallowMultipleComponent]
    public class TranslationProvider : SubProviderBase<Matrix4x4>
    {
        protected override bool RefreshAlways => true;

        [SerializeField] [HideInInspector] bool hasLegacyOneVectors = true;
        [FormerlySerializedAs("oneX")]
        [SerializeField] [HideInInspector] Vector3 legacyOneX = Vector3.right;
        [FormerlySerializedAs("oneY")]
        [SerializeField] [HideInInspector] Vector3 legacyOneY = Vector3.up;
        [FormerlySerializedAs("oneZ")]
        [SerializeField] [HideInInspector] Vector3 legacyOneZ = Vector3.forward;

        [Matrix4x4Custom]
        public Matrix4x4 translation = Matrix4x4.identity;

        void OnValidate()
        {
            if (hasLegacyOneVectors)
            {
                OneX = legacyOneX;
                OneY = legacyOneY;
                OneZ = legacyOneZ;
                hasLegacyOneVectors = false;
            }
        }

        public Vector3 OneX
        {
            get => translation.GetColumn(0);
            set
            {
                var m = translation;
                m.SetColumn(0, value);
                translation = m;
            }
        }

        public Vector3 OneY
        {
            get => translation.GetColumn(1);
            set
            {
                var m = translation;
                m.SetColumn(1, value);
                translation = m;
            }
        }

        public Vector3 OneZ
        {
            get => translation.GetColumn(2);
            set
            {
                var m = translation;
                m.SetColumn(2, value);
                translation = m;
            }
        }

        protected override Matrix4x4 CreateObject() => LocalTranslation(transform.ToLocalMatrix());

        public Matrix4x4 LocalTranslation(Matrix4x4 localMatrix) => localMatrix * translation;
    }
}