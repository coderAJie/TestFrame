using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoCoroutine
{
    private GameObject _obj;                            //gameobject 节点
    private MonoCoroutineItem _item;                    //Mono脚本
    private IEnumerator _enumerator;                    //协程
    private Action<MonoCoroutine> _completeCallback;    //完成回调

    public IEnumerator Enumerator { get => _enumerator; }

    public void Init(GameObject obj, IEnumerator enumerator, Action<MonoCoroutine> completeCallback)
    {
        _obj = obj;
        _item = _obj.AddComponent<MonoCoroutineItem>();
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

    public void Release()
    {
        _item.StopAllCoroutines();
        GameObject.Destroy(_obj);
        _obj = null;
        _item = null;
        _enumerator = null;
        _completeCallback = null;
    }
}

public class MonoCoroutineItem : MonoBehaviour
{
}
