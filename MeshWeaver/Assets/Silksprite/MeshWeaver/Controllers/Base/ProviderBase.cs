using System.Collections.Generic;
using Silksprite.MeshWeaver.Controllers.Context;
using UnityEditor;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Base
{
    internal static class ProviderBase
    {
        public static int Version { get; private set; }

        static ProviderBase()
        {
            EditorApplication.hierarchyChanged += OnHierarchyChanged;
        }

        static void OnHierarchyChanged()
        {
            Version++;
        }
    }
    

    public abstract class ProviderBase<T> : MonoBehaviour
    where T : class
    {
        protected virtual bool RefreshAlways => false;
        protected virtual bool RefreshOnHierarchyChanged => false;
        
        T _cachedObject;
        int _lastVersion;
        IMeshContext _lastContext;

        protected T CachedObject => _cachedObject;

        protected T FindOrCreateObject(IMeshContext context)
        {
            if (RefreshAlways)
            {
                return _cachedObject = CreateObject(context);
            }

            if (RefreshOnHierarchyChanged && _lastVersion != ProviderBase.Version)
            {
                _cachedObject = null;
                _lastVersion = ProviderBase.Version;
            }
            if (_lastContext != context)
            {
                _cachedObject = null;
                _lastContext = context;
            }
            if (_cachedObject != null)
            {
                foreach (var obj in _unityReferences)
                {
                    if (obj) continue;
                    _cachedObject = null;
                    break;
                }
            }

            if (_cachedObject != null) return _cachedObject;
            _unityReferences.Clear();
            RefreshUnityReferences();
            return _cachedObject = CreateObject(context);
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

        protected abstract T CreateObject(IMeshContext context);

        protected virtual void RefreshUnityReferences() { }
    }
}