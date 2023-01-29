using Silksprite.MeshWeaver.Controllers.Extensions;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Core
{
    public class TranslationCollector
    {
        public Matrix4x4 Translate(Matrix4x4 baseTranslation, Transform source)
        {
            return source ? source.ToLocalTranslation() * baseTranslation : baseTranslation;
        }
    }
}