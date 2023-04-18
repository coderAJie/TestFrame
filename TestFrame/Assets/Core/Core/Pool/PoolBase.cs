using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace SumBorn.Core
{
    public abstract class PoolBase<T> : IPool<T>
    {
        private Stack<T> _stack = new Stack<T>();

        public T Get()
        {
            if (_stack.Count == 0) _stack.Push(Create());
            T o = _stack.Pop();
            Initialize(o);
            return o;
        }

        public void Push(T o)
        {
            Destroy(o);
            _stack.Push(o);
        }

        public virtual void Clear(bool isReset=true)
        {
            if (isReset)
            {
                Debug.Log("isReset");
                foreach (T t in _stack)
                    Destroy(t);
            }
            _stack.Clear();
        }

        protected virtual T Create()
        {
            return default(T);
        }

        protected virtual void Initialize(T o)
        {
        }

        protected virtual void Destroy(T o)
        {
        }
    }
}
