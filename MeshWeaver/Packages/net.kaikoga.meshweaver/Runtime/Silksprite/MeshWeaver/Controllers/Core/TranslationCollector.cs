using Silksprite.MeshWeaver.Controllers.Extensions;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Core
{
    public class TranslationCollector
    {
        bool _hasTranslation;
        Matrix4x4 _translation = Matrix4x4.identity;
        int _revision;

        public int Sync(Transform source)
        {
            if (source)
            {
                _hasTranslation = true;
                _translation = source.ToLocalTranslation();
                if (MeshWeaverApplication.IsSelected(source.gameObject))
                {
                    _revision = MeshWeaverApplication.EmitRevision();
                }
                return _revision;
            }
            else
            {
                var lastValue = _hasTranslation; 
                _hasTranslation = false;
                return -1;
            }
        }

        public Matrix4x4 Translate(Matrix4x4 baseTranslation)
        {
            return _hasTranslation ? _translation * baseTranslation : baseTranslation;
        }
    }
}