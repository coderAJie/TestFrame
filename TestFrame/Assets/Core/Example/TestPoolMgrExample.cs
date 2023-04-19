using SumBorn.Core;
using SumBorn.Manager;
using System.Collections.Generic;
using UnityEngine;


public class TestPoolMgrExample : MonoBehaviour
{
    private Pool<GameObject> _pool;
    private List<GameObject> _list = new List<GameObject>();
    private Transform _objListTrans;

    void Start()
    {
        PoolMgr.Instance.Log();

        //_objListTrans = new GameObject("_objListTrans").transform;
        //_objListTrans.SetParent(transform);
        //_pool = new Pool<GameObject>(OnCreate, OnGet, OnPush);
    }

    private GameObject OnCreate()
    {
        GameObject o = GameObject.CreatePrimitive(PrimitiveType.Cube);
        o.transform.SetParent(_objListTrans);
        return o;
    }

    private void OnGet(GameObject o)
    {
        o.SetActive(true);
        o.name = "PoolCube";
    }

    private void OnPush(GameObject o)
    {
        o.SetActive(false);
        o.name = "PoolCubedisabled";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (_pool != null)
            {
                GameObject o = _pool.Get();
                _list.Add(o);
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (_list.Count > 0 && _pool != null)
            {
                GameObject o = _list[0];
                _list.RemoveAt(0);
                _pool.Push(o);
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_pool != null)
            {
                _pool.Clear();
                _pool = null;
                Destroy(_objListTrans.gameObject);
            }
        }
    }
}
