using System;

namespace SumBorn.Core
{
    public class Pool<T> : PoolBase<T>
    {
        Func<T> _onCreate;
        Action<T> _onGet;
        Action<T> _onPush;

        public Pool(Func<T> onCreate, Action<T> onGet, Action<T> onPush)
        {
            _onCreate = onCreate;
            _onGet = onGet;
            _onPush = onPush;
        }

        public override void Clear(bool isReset = true)
        {
            base.Clear(isReset);
            _onCreate = null;
            _onGet = null;
            _onPush = null;
        }

        protected override T OnCreate()
        {
            return _onCreate == null ? base.OnCreate() : _onCreate.Invoke();
        }

        protected override void OnGet(T o)
        {
            _onGet?.Invoke(o);
        }

        protected override void OnPush(T o)
        {
            _onPush?.Invoke(o);
        }
    }
}


