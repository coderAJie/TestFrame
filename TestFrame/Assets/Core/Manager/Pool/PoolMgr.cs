using SumBorn.Core;
using System;
using UnityEngine;

namespace SumBorn.Manager
{
    public class PoolMgr : Singleton<PoolMgr>
    {
        public ObjectPool GetObjectPool(GameObject prefab,Action<GameObject> onGet,Action<GameObject> onPush)
        {
            GameObject o = new GameObject(typeof(ObjectPool).ToString() +":"+ prefab.name);
            o.transform.SetParent(SingletonTrans);
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

