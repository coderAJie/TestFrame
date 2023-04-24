using SumBorn.Manager;
using System.Collections.Generic;
using UnityEngine;


public class TestPoolMgrExample : MonoBehaviour
{
    private GameObject _prefab;

    private ObjectPool _pool;
    private ObjectPool _pool2;

    private List<GameObject> _list = new List<GameObject>();
    private List<GameObject> _list2 = new List<GameObject>();

    void Start()
    {
        _prefab = GameObject.CreatePrimitive(PrimitiveType.Cube);

        _pool = PoolMgr.Instance.GetObjectPool(_prefab, (o) =>
        {
            o.name = "PoolCube";
        }, (o) =>
        {
            o.name = "PoolCubedisabled";
        });

        _pool2 = PoolMgr.Instance.GetObjectPool(_prefab, (o) =>
        {
            o.name = "PoolCube2";
        }, (o) =>
        {
            o.name = "PoolCubedisabled2";
        });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (_pool != null)
            {
                _list.Add(_pool.Get());
            }

            if (_list2.Count > 0 && _pool2 != null)
            {
                GameObject o = _list2[0];
                _list2.RemoveAt(0);
                _pool2.Push(o);
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

            if (_pool2 != null)
            {
                _list2.Add(_pool2.Get());
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_pool != null)
            {
                _pool.Clear();
            }

            if (_pool2 != null)
            {
                _pool2.Clear();
            }
        }
    }
}
