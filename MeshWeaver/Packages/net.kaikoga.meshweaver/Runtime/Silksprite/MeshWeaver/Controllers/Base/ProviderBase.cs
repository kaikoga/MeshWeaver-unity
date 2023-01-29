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
        protected T CachedObject { get; private set; }

        protected T FindOrCreateObject()
        {
            Sync();
            return CachedObject = CreateObject();
        }

        void OnValidate()
        {
            // NOTE: This only work for ModiferProviders, because GeometryProviders refer Hierarchy structures
            if (serializedFormat != CurrentSerializedFormat)
            {
                Upgrade(serializedFormat, CurrentSerializedFormat);
                serializedFormat = CurrentSerializedFormat;
#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(this);
#endif
            }
        }

        protected virtual void Sync() { }
        protected abstract T CreateObject();
    }
}