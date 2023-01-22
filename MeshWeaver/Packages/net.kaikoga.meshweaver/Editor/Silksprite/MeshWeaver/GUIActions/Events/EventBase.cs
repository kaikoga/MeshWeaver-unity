using System;
using System.Collections.Generic;

namespace Silksprite.MeshWeaver.GUIActions.Events
{
    public abstract class EventBase : IDisposable
    {
        public abstract void Dispose();
    }

    public static class EventPool<T>
        where T : new()
    {
        public static readonly Stack<T> Pool = new Stack<T>();

        public static T GetPooled()
        {
            return Pool.Count > 0 ? Pool.Pop() : new T();
        }
    }

    public abstract class EventBase<T> : EventBase
        where T : EventBase<T>, new()
    {
        public override void Dispose()
        {
            EventPool<T>.Pool.Push((T)this);
        }
    }

    public abstract class EventBase<T, TPayload> : EventBase
        where T : EventBase<T, TPayload>, new()
    {
        public TPayload payload;

        public override void Dispose()
        {
            EventPool<T>.Pool.Push((T)this);
        }
    }
}