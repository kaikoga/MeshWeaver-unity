using System.Collections.Generic;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Base
{
    public abstract class ProviderBase<T> : MonoBehaviour
    where T : class
    {
        T _cachedObject;

        protected T CachedObject
        {
            get
            {
                foreach (var obj in _unityReferences)
                {
                    if (obj) continue;
                    _cachedObject = null;
                    break;
                }

                if (_cachedObject != null) return _cachedObject;
                _unityReferences.Clear();
                RefreshUnityReferences();
                return _cachedObject = CreateObject();
            }
        }

        readonly List<Object> _unityReferences = new List<Object>();

        void OnValidate()
        {
            // XXX: Modifiers don't need it, but some Providers should also check hierarchy changes
            _cachedObject = null;
        }

        protected void AddUnityReference(Object obj)
        {
            if (obj) _unityReferences.Add(obj);
        }

        protected abstract T CreateObject();

        protected virtual void RefreshUnityReferences() { }
    }
}