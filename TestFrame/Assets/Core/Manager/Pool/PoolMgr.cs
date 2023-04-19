using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SumBorn.Core;
using System;

namespace SumBorn.Manager
{
    public class PoolMgr : Singleton<PoolMgr>
    {
        private readonly string ObjectPoolName = "[ObjectPool]:";
        public ObjectPool GetObjectPool(GameObject prefab,Action<GameObject> onGet,Action<GameObject> onPush)
        {
            GameObject o = new GameObject(ObjectPoolName + prefab.name);
            ObjectPool pool = o.AddComponent<ObjectPool>();
            pool.InitPool(prefab, onGet, onPush);
            return pool;
        }

        public Pool<T> GetPool<T>(Func<T> onCreate,Action<T> onGet,Action<T> onPush)
        {
            return new Pool<T>(onCreate, onGet, onPush);
        }
    }
}

