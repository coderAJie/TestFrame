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
    void Start()
    {
        TestSingletonMgr.Instance.TestDebug();
        SingletonMgr.Instance.Release(TestSingletonMgr.Instance);
    }
}
