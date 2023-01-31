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
        int _globalFrame = -1;

        protected T CachedObject { get; private set; }

        public int Revision
        {
            get
            {
                if (_globalFrame != MeshWeaverApplication.globalFrame)
                {
                    Sync();
                    CachedObject = default;
                    _globalFrame = MeshWeaverApplication.globalFrame;
                }
                return _globalFrame;
            }
        }

        protected T FindOrCreateObject()
        {
            var _ = Revision;
            return CachedObject = CreateObject();
        }

        void OnValidate()
        {
            // catch Inspector changes
            _globalFrame = -1;
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

        protected virtual int Sync() => 0;
        protected abstract T CreateObject();
    }
}