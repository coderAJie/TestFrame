using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SumBorn.Core
{
    public interface IPool<T>
    {
        T Get();
        void Push(T o);
        void Clear(bool iReset = true);
    }
}