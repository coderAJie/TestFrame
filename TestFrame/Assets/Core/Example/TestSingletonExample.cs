using SumBorn.Core;
using UnityEngine;

public class TestSingletonMgr : Singleton<TestSingletonMgr>
{
    public override void Initialize()
    {
        base.Initialize();
    }

    public void TestDebug()
    {
        Debug.Log("TestDebug");
    }
}

public class TestSingletonExample : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TestSingletonMgr.Instance.TestDebug();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            SingletonMgr.Instance.Release(TestSingletonMgr.Instance);
        }
    }
}
