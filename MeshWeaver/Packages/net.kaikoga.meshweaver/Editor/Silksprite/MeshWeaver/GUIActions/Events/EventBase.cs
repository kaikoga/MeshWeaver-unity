using System;
using System.Collections.Generic;

namespace Silksprite.MeshWeaver.GUIActions.Events
{
    public abstract class EventBase : IDisposable
    {
        public abstract void Dispose();
    }

    public abstract class EventBase<T> : EventBase
        where T : EventBase<T>, new()
    {
        static readonly Stack<T> Pool = new Stack<T>();

        public static T GetPooled()
        {
            return Pool.Count > 0 ? Pool.Pop() : new T();
        }
        
        public override void Dispose()
        {
            Pool.Push((T)this);
        }
    }
}