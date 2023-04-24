using UnityEngine;

namespace SumBorn.Core
{
    public interface ISingleton
    {
        Transform SingletonTrans { get; set; }
        void Initialize();
        void Release();
    }
}
