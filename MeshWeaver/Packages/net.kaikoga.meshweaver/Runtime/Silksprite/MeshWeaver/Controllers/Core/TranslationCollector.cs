using Silksprite.MeshWeaver.Controllers.Extensions;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Core
{
    public class TranslationCollector
    {
        bool _hasTranslation;
        Matrix4x4 _translation = Matrix4x4.identity;

        public bool Sync(Transform source)
        {
            if (source)
            {
                _hasTranslation = true;
                _translation = source.ToLocalTranslation();
                return MeshWeaverApplication.IsSelected(source.gameObject);
            }
            else
            {
                var lastValue = _hasTranslation; 
                _hasTranslation = false;
                return lastValue;
            }
        }

        public Matrix4x4 Translate(Matrix4x4 baseTranslation)
        {
            return _hasTranslation ? _translation * baseTranslation : baseTranslation;
        }
    }
}