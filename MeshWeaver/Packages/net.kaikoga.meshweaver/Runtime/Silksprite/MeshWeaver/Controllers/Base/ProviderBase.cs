using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers.Base
{
    public abstract class ProviderBase : MonoBehaviour
    {
        protected const int CurrentSerializedFormat = 1;

        [HideInInspector]
        [SerializeField] protected int serializedFormat = CurrentSerializedFormat;

        protected virtual void Upgrade(int oldVersion, int newVersion) { }
    }

    public abstract class ProviderBase<T> : ProviderBase
    {
        bool _hasCachedValue;
        protected T CachedObject { get; private set; }

        protected T FindOrCreateObject()
        {
            if (Sync())
            {
                _hasCachedValue = false;
                CachedObject = default;
            }

            return _hasCachedValue ? CachedObject : CachedObject = CreateObject();
        }

        void OnValidate()
        {
            // catch Inspector changes
            _hasCachedValue = false;
            CachedObject = default;

            if (serializedFormat != CurrentSerializedFormat)
            {
                Upgrade(serializedFormat, CurrentSerializedFormat);
                serializedFormat = CurrentSerializedFormat;
#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(this);
#endif
            }
        }

        protected virtual bool Sync() => false;
        protected abstract T CreateObject();
    }
}