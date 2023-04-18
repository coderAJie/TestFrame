using SumBorn.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
using UnityEngine;

public class TestPoolExample : MonoBehaviour
{
    private Pool<GameObject> _pool;
    private List<GameObject> _list=new List<GameObject>();

    void Start()
    {
        _pool = new Pool<GameObject>(OnCreate, OnInitialize, OnReset);
    }

    private GameObject OnCreate()
    {
        GameObject o = GameObject.CreatePrimitive(PrimitiveType.Cube);
        o.transform.SetParent(this.transform);
        return o;
    }

    private void OnInitialize(GameObject o)
    {
        o.SetActive(true);
        o.name = "PoolCube";
    }

    private void OnReset(GameObject o)
    {
        o.SetActive(false);
        o.name = "PoolCubedisabled";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameObject o = _pool.Get();
            _list.Add(o);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (_list.Count > 0)
            {
                GameObject o = _list[0];
                _list.RemoveAt(0);
                _pool.Push(o);
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            _pool.Clear();
        }
    }
}
