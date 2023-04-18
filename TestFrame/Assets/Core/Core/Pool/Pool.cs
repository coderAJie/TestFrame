using SumBorn.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace SumBorn.Core
{
    public class Pool<T> : PoolBase<T>
    {
        Func<T> _onCreate;
        Action<T> _onInitialize;
        Action<T> _onDestroy;

        public Pool(Func<T> onCreate, Action<T> onInitialize, Action<T> onDestroy)
        {
            _onCreate = onCreate;
            _onInitialize = onInitialize;
            _onDestroy = onDestroy;
        }

        public override void Clear(bool isReset = true)
        {
            base.Clear(isReset);
            _onCreate = null;
            _onInitialize = null;
            _onDestroy = null;
        }

        protected override T Create()
        {
            return _onCreate == null ? base.Create() : _onCreate.Invoke();
        }

        protected override void Initialize(T o)
        {
            _onInitialize?.Invoke(o);
        }

        protected override void Destroy(T o)
        {
            _onDestroy?.Invoke(o);
        }
    }
}


