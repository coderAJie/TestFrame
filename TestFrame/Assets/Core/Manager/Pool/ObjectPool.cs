using System;
using SumBorn.Core;
using UnityEngine;

namespace SumBorn.Manager
{
    public class ObjectPool : MonoBehaviour
    {
        private GameObject _prefab;
        private Pool<GameObject> _pool;

        private Action<GameObject> _onGet;
        private Action<GameObject> _onPush;

        public void InitPool(GameObject prefab, Action<GameObject> onGet = null, Action<GameObject> onPush = null)
        {
            _prefab = prefab;
            _onGet = onGet;
            _onPush = onPush;
            _pool = new Pool<GameObject>(OnCreate, OnGet, OnPush);
        }

        public GameObject Get()
        {
            return _pool.Get();
        }

        public void Push(GameObject o)
        {
            _pool.Push(o);   
        }

        public void Clear(bool invokePushAction = true)
        {
            _pool.Clear(invokePushAction);
            _pool = null;
            _onGet = null;
            _onPush = null;
            GameObject.Destroy(this.gameObject);
        }

        private GameObject OnCreate()
        {
            return GameObject.Instantiate(_prefab);
        }

        private void OnGet(GameObject o)
        {
            o.transform.SetParent(this.transform);
            o.SetActive(true);
            _onGet?.Invoke(o);
        }

        private void OnPush(GameObject o)
        {
            o.SetActive(false);
            _onPush?.Invoke(o);
        }
    }
}
