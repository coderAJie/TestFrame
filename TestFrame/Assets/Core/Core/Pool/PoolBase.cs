using System.Collections.Generic;

namespace SumBorn.Core
{
    public abstract class PoolBase<T> : IPool<T>
    {
        private Stack<T> _stack = new Stack<T>();

        public T Get()
        {
            if (_stack.Count == 0) _stack.Push(OnCreate());
            T o = _stack.Pop();
            OnGet(o);
            return o;
        }

        public void Push(T o)
        {
            OnPush(o);
            _stack.Push(o);
        }

        public virtual void Clear(bool invokePushAction = true)
        {
            if (invokePushAction)
            {
                foreach (T t in _stack)
                    OnPush(t);
            }
            _stack.Clear();
        }

        protected virtual T OnCreate()
        {
            return default(T);
        }

        protected virtual void OnGet(T o)
        {
        }

        protected virtual void OnPush(T o)
        {
        }
    }
}
