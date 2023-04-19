using System;
using UnityEngine;

namespace SumBorn.Manager
{
    public class ObjectPool : MonoBehaviour
    {
        private string _name;
        private GameObject _prefab;
        private Transform _parentTrans;

        public ObjectPool(GameObject prefab, Func<GameObject> onCreate, Action<GameObject> onGet, Action<GameObject> onPush, Transform parentTrans = null)
        {
            _prefab = prefab;
            _parentTrans = parentTrans;
        }

        public void Get(GameObject o)
        {
        }

        public void Push(GameObject o)
        {
        }

        public void Clear(bool isReset = true)
        {
        }

        private GameObject Create()
        {
            return null;
        }
    }
}
