using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class SingletonMgr : Singleton<SingletonMgr>
{
    private Transform _rootTrans;
    private bool _isInitialize = false;
    private Dictionary<ISingleton, Transform> _dic = new Dictionary<ISingleton, Transform>();

    public void Initialize(ISingleton singleton)
    {
        if (singleton == this)
        {
            if (_isInitialize) return;
            _isInitialize = true;
            _rootTrans = new GameObject(singleton.GetType().ToString()).transform;
            _dic.Add(singleton, _rootTrans);
        }
        else
        {
            singleton.Initialize();
            Transform o = new GameObject(singleton.GetType().ToString()).transform;
            o.transform.SetParent(_rootTrans);
            _dic.Add(singleton, o);
        }
    }

    public void Release(ISingleton singleton)
    {
        if (_dic.ContainsKey(singleton))
        {
            singleton.Release();
            GameObject.Destroy(_dic[singleton].gameObject);
            _dic.Remove(singleton);
        }
    }
}