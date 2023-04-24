using UnityEngine;

namespace SumBorn.Core
{
    public abstract class Singleton<T> : ISingleton where T : new()
    {
        public Transform SingletonTrans { get; set; }

        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T();
                    SingletonMgr.Instance.Initialize(_instance as ISingleton);
                }
                return _instance;
            }
        }

        public virtual void Initialize()
        {

        }

        public virtual void Release()
        {
            _instance = default(T);
        }
    }
}

