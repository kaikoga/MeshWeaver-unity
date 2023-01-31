using System;
using System.Collections.Generic;

namespace Silksprite.MeshWeaver.Controllers.Core
{
    public static class Collectors
    {
        static readonly Stack<object> Stack = new Stack<object>();

        public static void Start(object collector)
        {
            if (Stack.Contains(collector))
            {
                Stack.Clear();
                throw new InvalidOperationException("Cyclic reference");
            }
            Stack.Push(collector);
        }

        public static void Finish(object collector)
        {
            if (Stack.Pop() != collector)
            {
                throw new InvalidOperationException("Internal Error");
            }
        }
    }
}