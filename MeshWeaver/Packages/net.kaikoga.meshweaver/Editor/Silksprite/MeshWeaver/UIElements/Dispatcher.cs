using UnityEngine.UIElements;

namespace Silksprite.MeshWeaver.UIElements
{
    public class Dispatcher<T> where T : EventBase<T>, new()
    {
        EventCallback<T> _onDispatch;

        public void Add(EventCallback<T> callback) => _onDispatch += callback;

        public void Invoke()
        {
            using (var evt = EventBase<T>.GetPooled()) _onDispatch?.Invoke(evt);
        }
    }
    
    namespace Extensions {
    }
}