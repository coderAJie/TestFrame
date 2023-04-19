using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SumBorn.Core;

namespace SumBorn.Manager
{
    public class PoolMgr : Singleton<PoolMgr>
    {
        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Release()
        {
            base.Release();
        }

        public void Log()
        {
            Debug.Log("1");
        }
    }
}

