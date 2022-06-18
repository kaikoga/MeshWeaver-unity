using System.Collections.Generic;
using Silksprite.MeshWeaver.Models;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Base.Modifiers
{
    public abstract class ModifierProviderBase<T> : MonoBehaviour
    where T : class
    {
        public LodMask lodMask = LodMask.All;
        
        public LodMask LodMask
        {
            get => lodMask;
            set => lodMask = value;
        }

        // ReSharper disable once Unity.RedundantEventFunction
        void Start()
        {
            // The sole reason for this empty method is for showing enabled checkbox
        }

        #region Modifier caching and invalidation

        T _cachedModifier;

        protected T CachedModifier
        {
            get
            {
                foreach (var obj in _unityReferences)
                {
                    if (obj) continue;
                    _cachedModifier = null;
                    break;
                }

                if (_cachedModifier != null) return _cachedModifier;
                _unityReferences.Clear();
                RefreshUnityReferences();
                return _cachedModifier = CreateModifier();
            }
        }

        readonly List<Object> _unityReferences = new List<Object>();

        void OnValidate()
        {
            // XXX: Modifiers don't need it, but some Providers should also check hierarchy changes
            _cachedModifier = null;
        }

        protected void AddUnityReference(Object obj)
        {
            if (obj) _unityReferences.Add(obj);
        }

        protected abstract T CreateModifier();

        protected virtual void RefreshUnityReferences() { }
        #endregion
    }
}