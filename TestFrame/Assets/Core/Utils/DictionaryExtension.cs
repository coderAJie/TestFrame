using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public static class DictionaryExtension
{
    public static void DeleteReverse<TK, T>(this Dictionary<TK,T> dic,Action<TK, T> action=null)
    {
        List<KeyValuePair<TK,T>> dicList=dic.ToList();
        dicList.DeleteReverse((pair) => { action?.Invoke(pair.Key, pair.Value); });
    }
}
