using SumBorn.Manager;
using System.Collections;
using UnityEngine;

public class TestMonoLifeExample : MonoBehaviour
{
    private int _updateCount = 0;
    private IEnumerator enumerator;

    void Start()
    {
        //Test Add/ Remove Update
        //MonoLifeMgr.Instance.AddUpdate(TestUpdateAction);

        //Test Add/Remove Coroutine
        //enumerator = ITestCoroutine1();
        //MonoLifeMgr.Instance.AddCoroutine(enumerator);

        //Test More Add/Remove Coroutine At Once
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
    private void TestCoroutine()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            MonoLifeMgr.Instance.RemoveUpdate(TestCoroutine);
            MonoLifeMgr.Instance.RemoveAllCoroutine();
        }
    }

    private IEnumerator ITestCoroutine1()
    {
        yield return null;
        Debug.Log("ITestCoroutine1");
        yield return new WaitForSeconds(3f);

        enumerator = ITestCoroutine2();
        MonoLifeMgr.Instance.AddCoroutine(enumerator);
    }

    private IEnumerator ITestCoroutine2()
    {
        yield return null;
        Debug.Log("ITestCoroutine2");
        yield return new WaitForSeconds(5f);
        Debug.Log("ITestCoroutine2_end");
    }
    #endregion
}
