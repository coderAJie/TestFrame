using SumBorn.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonoLifeMgr : Singleton<MonoLifeMgr>
{
    private GameObject _mgrObj;                                     //gameobject 根节点
    private MonoLifeItem _monoItem;                                 //gameobject Mono脚本
    private Dictionary<IEnumerator, MonoCoroutine> _coroutineDic;   //协程字典
    private Pool<MonoCoroutine> _poolCtrl;                      //协程对象池

    public override void Initialize()
    {
        base.Initialize();

        _mgrObj = new GameObject("MonoLifeMgr");
        _monoItem = _mgrObj.AddComponent<MonoLifeItem>();
        _coroutineDic = new Dictionary<IEnumerator, MonoCoroutine>();
        _poolCtrl = new Pool<MonoCoroutine>(null,null,null) { };
    }

    public override void Release()
    {
        base.Release();

        RemoveAllCoroutine();
        _coroutineDic = null;
        _poolCtrl.Clear();
        _monoItem = null;
        GameObject.Destroy(_mgrObj);
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
        GameObject obj = new GameObject("MonoCoroutineItem");
        obj.transform.SetParent(_mgrObj.transform);
        MonoCoroutine monoCoroutine = _poolCtrl.Get();
        monoCoroutine.Init(obj, enumerator, (action) => { RemoveCoroutine(enumerator); });
        _coroutineDic.Add(enumerator, monoCoroutine);
    }

    public void RemoveCoroutine(IEnumerator enumerator)
    {
        if (enumerator == null) return;
        if(_coroutineDic.TryGetValue(enumerator,out MonoCoroutine monoCoroutine))
        {
            _coroutineDic.Remove(enumerator);
            _poolCtrl.Push(monoCoroutine);
        }
    }

    public void RemoveAllCoroutine()
    {
        _coroutineDic.DeleteReverse((key,value) => {
            _poolCtrl.Push(value);
        });
    }

    private MonoCoroutine OnCoroutineCreate()
    {
        return new MonoCoroutine();
    }

    private void OnCoroutineReset(MonoCoroutine monoCoroutine) {
        monoCoroutine.Release();
    }
}
