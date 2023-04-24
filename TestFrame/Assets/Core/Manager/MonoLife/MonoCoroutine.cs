using System;
using System.Collections;
using UnityEngine;

namespace SumBorn.Manager
{
    public class MonoCoroutine
    {
        private GameObject _obj;                            //gameobject 节点
        private MonoCoroutineItem _item;                    //Mono脚本
        private IEnumerator _enumerator;                    //协程
        private Action<MonoCoroutine> _completeCallback;    //完成回调

        public IEnumerator Enumerator { get => _enumerator; }

        public MonoCoroutine()
        {
            _obj = new GameObject(typeof(MonoCoroutine).ToString());
            _obj.transform.SetParent(MonoLifeMgr.Instance.SingletonTrans);
            _item = _obj.AddComponent<MonoCoroutineItem>();
        }

        public void Init(IEnumerator enumerator, Action<MonoCoroutine> completeCallback)
        {
            _enumerator = enumerator;
            _completeCallback = completeCallback;
            _item.StartCoroutine(IStartCoroutine());
        }

        private IEnumerator IStartCoroutine()
        {
            yield return _enumerator;
            if (_completeCallback != null)
            {
                _completeCallback(this);
            }
        }

        public void OnGet()
        {
            _obj.SetActive(true);
        }

        public void OnPush()
        {
            _item.StopAllCoroutines();
            _obj.SetActive(false);
            _enumerator = null;
            _completeCallback = null;
        }
    }

    public class MonoCoroutineItem : MonoBehaviour
    {
    }
}