using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ListExtension
{
    public static void DeleteReverse<T>(this List<T> list, Action<T> action=null)
    {
        for (int i = list.Count - 1; i >= 0; i--)
        {
            action?.Invoke(list[i]);
            list.Remove(list[i]);
        }
    }
}
