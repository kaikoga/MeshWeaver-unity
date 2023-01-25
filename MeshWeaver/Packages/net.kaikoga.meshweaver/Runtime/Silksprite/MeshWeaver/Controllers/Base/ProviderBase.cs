using System.Collections.Generic;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Base
{
    public abstract class ProviderBase : MonoBehaviour
    {
        protected const int CurrentSerializedFormat = 1;

        [HideInInspector]
        [SerializeField] protected int serializedFormat = CurrentSerializedFormat;

        protected virtual bool RefreshAlways => false;

        protected virtual void Upgrade(int oldVersion, int newVersion) { }
    }

    public abstract class ProviderBase<T> : ProviderBase
    {
        bool _hasCachedObject;
        protected T CachedObject { get; private set; }

        protected T FindOrCreateObject()
        {
            if (RefreshAlways)
            {
                return CachedObject = CreateObject();
            }

            if (_hasCachedObject)
            {
                foreach (var obj in _unityReferences)
                {
                    if (obj) continue;
                    CachedObject = default;
                    _hasCachedObject = false;
                    break;
                }
            }

            if (_hasCachedObject) return CachedObject;
            _unityReferences.Clear();
            RefreshUnityReferences();
            _hasCachedObject = true;
            return CachedObject = CreateObject();
        }

        readonly List<Object> _unityReferences = new List<Object>();

        void OnValidate()
        {
            // NOTE: This only work for ModiferProviders, because GeometryProviders refer Hierarchy structures
            CachedObject = default;
            _hasCachedObject = false;
            if (serializedFormat != CurrentSerializedFormat)
            {
                Upgrade(serializedFormat, CurrentSerializedFormat);
                serializedFormat = CurrentSerializedFormat;
#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(this);
#endif
            }
        }

        protected void AddUnityReference(Object obj)
        {
            if (obj) _unityReferences.Add(obj);
        }

        protected abstract T CreateObject();

        protected virtual void RefreshUnityReferences() { }
    }
}