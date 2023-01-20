using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Extensions
{
    public static class TransformExtension
    {
        public static Matrix4x4 ToLocalMatrix(this Transform transform)
        {
            return Matrix4x4.TRS(transform.localPosition, transform.localRotation, transform.localScale);
        }

        public static Matrix4x4 ToLocalTranslation(this Transform transform)
        {
            var localTranslation = transform.ToLocalMatrix();
            if (!(transform.TryGetComponent<TranslationProvider>(out var translation) && translation.enabled))
            {
                return localTranslation;
            }
            return translation.LocalTranslation(localTranslation);
        }

        public static Matrix4x4 ToLocalTranslationModification(this Transform transform)
        {
            var localTranslation = Matrix4x4.identity;
            if (!(transform.TryGetComponent<TranslationProvider>(out var translation) && translation.enabled))
            {
                return localTranslation;
            }
            return translation.LocalTranslation(localTranslation);
        }
    }
}