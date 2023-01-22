namespace Silksprite.MeshWeaver.GUIActions.Events
{
    public class Dispatcher<T> where T : EventBase<T>, new()
    {
        EventCallback<T> _onDispatch;

        public void Add(EventCallback<T> callback) => _onDispatch += callback;

        public void Invoke()
        {
            using (var evt = EventPool<T>.GetPooled()) _onDispatch?.Invoke(evt);
        }
    }

    public class Dispatcher<T, TPayload> where T : EventBase<T, TPayload>, new()
    {
        EventCallback<T> _onDispatch;

        public void Add(EventCallback<T> callback) => _onDispatch += callback;

        public void Invoke(TPayload payload)
        {
            using (var evt = EventPool<T>.GetPooled())
            {
                evt.payload = payload;
                _onDispatch?.Invoke(evt);
                evt.payload = default;
            }
        }
    }

}