using System.Collections.Generic;
using Silksprite.MeshWeaver.Controllers.Context;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Base
{
    public abstract class ProviderBase<T> : MonoBehaviour
    where T : class
    {
        const int CurrentSerializedFormat = 1;

        [SerializeField] int serializedFormat = CurrentSerializedFormat;

        protected virtual bool RefreshAlways => false;

        IMeshContext _lastContext;

        protected T CachedObject { get; private set; }

        protected T FindOrCreateObject(IMeshContext context)
        {
            if (RefreshAlways)
            {
                return CachedObject = CreateObject(context);
            }

            if (_lastContext != context)
            {
                CachedObject = null;
                _lastContext = context;
            }
            if (CachedObject != null)
            {
                foreach (var obj in _unityReferences)
                {
                    if (obj) continue;
                    CachedObject = null;
                    break;
                }
            }

            if (CachedObject != null) return CachedObject;
            _unityReferences.Clear();
            RefreshUnityReferences();
            return CachedObject = CreateObject(context);
        }

        readonly List<Object> _unityReferences = new List<Object>();

        void OnValidate()
        {
            // NOTE: This only work for ModiferProviders, because GeometryProviders refer Hierarchy structures
            CachedObject = null;
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

        protected abstract T CreateObject(IMeshContext context);

        protected virtual void RefreshUnityReferences() { }
        
        protected virtual void Upgrade(int oldVersion, int newVersion) { }

    }
}