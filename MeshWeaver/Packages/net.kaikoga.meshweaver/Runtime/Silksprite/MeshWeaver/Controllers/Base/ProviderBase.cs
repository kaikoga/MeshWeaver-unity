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
        int _myRevision = -1;
        int _revision = -1;
        int _cachedRevision = -1;
        int _globalFrame = -1;

        protected T CachedObject { get; private set; }

        public int Revision
        {
            get
            {
                if (_globalFrame == MeshWeaverApplication.globalFrame) return _revision;

                _globalFrame = MeshWeaverApplication.globalFrame;
                _revision = _myRevision ^ Sync();
                return _revision;
            }
        }

        protected T FindOrCreateObject()
        {
            // catch Transform changes
            if (MeshWeaverApplication.IsSelected(gameObject)) _myRevision = MeshWeaverApplication.EmitRevision();
            
            // catch Unity / ProviderBase reference changes
            var newRevision = Revision;
            if (newRevision == _cachedRevision) return CachedObject;
            _cachedRevision = newRevision;
            return CachedObject = CreateObject();
        }

        void OnValidate()
        {
            // catch Inspector changes
            _myRevision = MeshWeaverApplication.EmitRevision();
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

        public virtual int Sync() => 0;
        protected abstract T CreateObject();
    }
}