using SumBorn.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMonoLifeExample : MonoBehaviour
{
    private int _updateCount = 0;
    void Start()
    {
        //Test Add/Remove Update
        //MonoLifeMgr.Instance.AddUpdate(TestUpdateAction);

        //Test Add/Remove Coroutine
        enumerator = ITestCoroutine1();
        MonoLifeMgr.Instance.AddCoroutine(enumerator);
        MonoLifeMgr.Instance.AddUpdate(TestCoroutine);

        for (int i = 0; i < 100; i++)
        {
            MonoLifeMgr.Instance.AddCoroutine(ITestCoroutine2());
        }
    }

    #region Test Add/Remove Update
    private void TestUpdateAction()
    {
        if (Time.frameCount % 500 == 0)
        {
            _updateCount++;
            Debug.Log("AddUpdate");
        }

        if (_updateCount == 3)
        {
            MonoLifeMgr.Instance.RemoveUpdate(TestUpdateAction);
            Debug.Log("RemoveUpdate");
        }
    }
    #endregion

    #region Test Add/Remove Coroutine
    private IEnumerator enumerator;
    private void TestCoroutine()
    {
        if (Time.frameCount % 500 == 0)
        {
            _updateCount++;
        }

        if (_updateCount == 2)
        {
            MonoLifeMgr.Instance.RemoveUpdate(TestCoroutine);
            MonoLifeMgr.Instance.RemoveCoroutine(enumerator);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            MonoLifeMgr.Instance.RemoveUpdate(TestCoroutine);
            SingletonMgr.Instance.Release(MonoLifeMgr.Instance as ISingleton);
        }
    }

    private IEnumerator ITestCoroutine1()
    {
        yield return null;
        Debug.Log("ITestCoroutine1");
        yield return new WaitForSeconds(1.2f);
    }

    private IEnumerator ITestCoroutine2()
    {
        yield return null;
        Debug.Log("ITestCoroutine2");
        yield return new WaitForSeconds(8f);
        Debug.Log("ITestCoroutine2_end");
    }
    #endregion
}
