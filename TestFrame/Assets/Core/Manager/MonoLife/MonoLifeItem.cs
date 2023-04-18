using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoLifeItem : MonoBehaviour
{
    private event Action _updateEvent;
    private event Action _lateUpdateEvent;
    private event Action _fixedUpdateEvent;

    public void AddUpdate(Action action)
    {
        _updateEvent += action;
    }

    public void RemoveUpdate(Action action)
    {
        _updateEvent -= action;
    }

    public void AddFixedUpdate(Action action)
    {
        _fixedUpdateEvent += action;
    }

    public void RemoveFixedUpdate(Action action)
    {
        _fixedUpdateEvent -= action;
    }

    public void AddLateUpdate(Action action)
    {
        _lateUpdateEvent += action;
    }

    public void RemoveLateUpdate(Action action)
    {
        _lateUpdateEvent -= action;
    }

    void Update()
    {
        _updateEvent?.Invoke();
    }

    void FixedUpdate()
    {
        _fixedUpdateEvent?.Invoke();
    }

    private void LateUpdate()
    {
        _lateUpdateEvent?.Invoke();
    }
}
