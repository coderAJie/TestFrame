using SumBorn.Core;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SumBorn.Manager
{
    public class MonoLifeMgr : Singleton<MonoLifeMgr>
    {
        private MonoLifeItem _monoItem;                                 //gameobject Mono�ű�
        private Dictionary<IEnumerator, MonoCoroutine> _coroutineDic;   //Э���ֵ�
        private Pool<MonoCoroutine> _poolCtrl;                      //Э�̶����

        public override void Initialize()
        {
            _monoItem = SingletonTrans.gameObject.AddComponent<MonoLifeItem>();
            _coroutineDic = new Dictionary<IEnumerator, MonoCoroutine>();
            _poolCtrl = new Pool<MonoCoroutine>(OnCoroutineCreate, OnCoroutineGet, OnCoroutinePush) { };
        }

        public override void Release()
        {
            RemoveAllCoroutine();
            _coroutineDic = null;
            _poolCtrl.Clear();
            _monoItem = null;

            base.Release();
        }

        public void AddUpdate(Action action)
        {
            _monoItem.AddUpdate(action);
        }

        public void RemoveUpdate(Action action)
        {
            _monoItem.RemoveUpdate(action);
        }

        public void AddLateUpdate(Action action)
        {
            _monoItem.AddLateUpdate(action);
        }

        public void RemoveLateUpdate(Action action)
        {
            _monoItem.RemoveLateUpdate(action);
        }

        public void AddFixedUpdate(Action action)
        {
            _monoItem.AddFixedUpdate(action);
        }

        public void RemoveFixedUpdate(Action action)
        {
            _monoItem.RemoveFixedUpdate(action);
        }

        public void AddCoroutine(IEnumerator enumerator)
        {
            MonoCoroutine monoCoroutine = _poolCtrl.Get();
            monoCoroutine.Init(enumerator, (action) => { RemoveCoroutine(enumerator); });
            _coroutineDic.Add(enumerator, monoCoroutine);
        }

        public void RemoveCoroutine(IEnumerator enumerator)
        {
            if (enumerator == null) return;
            if (_coroutineDic.TryGetValue(enumerator, out MonoCoroutine monoCoroutine))
            {
                _coroutineDic.Remove(enumerator);
                _poolCtrl.Push(monoCoroutine);
            }
        }

        public void RemoveAllCoroutine()
        {
            _coroutineDic.DeleteReverse((key, value) =>
            {
                _poolCtrl.Push(value);
            });
        }

        private MonoCoroutine OnCoroutineCreate()
        {
            return new MonoCoroutine();
        }

        private void OnCoroutineGet(MonoCoroutine monoCoroutine)
        {
            monoCoroutine.OnGet();
        }

        private void OnCoroutinePush(MonoCoroutine monoCoroutine)
        {
            monoCoroutine.OnPush();
        }
    }
}