using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    #region DateTime
    public static string GetDate(long time)
    {
        System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
        DateTime dt = startTime.AddMilliseconds(time);
        string t = dt.ToString("yyyy/MM/dd HH:mm");
        return t;
    }

    public static long GetTimeTicks(DateTime time)
    {
        return (time.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
    }
    #endregion

    #region String
    /// <summary>
    /// 限制替换string字节长度
    /// 例如：EllipsisString("人a1_——打", 7, "...")
    /// 输出：人a1_—...
    /// </summary>
    /// <param name="value"></param>
    /// <param name="limit"></param>
    /// <param name="ellipsis"></param>
    /// <returns></returns>
    public static string EllipsisString(string value, int limit, string ellipsis)
    {
        string outputStr = value;
        outputStr = LimitStringByUTF8(value, limit) + (CheckStringByUTF8(value, limit + 1) ? ellipsis : "");
        return outputStr;
    }
    public static bool CheckStringByUTF8(string temp, int limit)
    {
        bool overflow = false;
        int count = 0;

        for (int i = 0; i < temp.Length; i++)
        {
            string tempStr = temp.Substring(i, 1);
            int byteCount = System.Text.ASCIIEncoding.UTF8.GetByteCount(tempStr);
            if (byteCount > 1)
            {
                count += 2;
            }
            else
            {
                count += 1;
            }
            if (count >= limit)
            {
                overflow = true;
            }
        }
        return overflow;
    }
    public static string LimitStringByUTF8(string temp, int limit)
    {
        string outputStr = "";
        int count = 0;

        for (int i = 0; i < temp.Length; i++)
        {
            string tempStr = temp.Substring(i, 1);
            int byteCount = System.Text.ASCIIEncoding.UTF8.GetByteCount(tempStr);
            if (byteCount > 1)
            {
                count += 2;
            }
            else
            {
                count += 1;
            }

            if (count <= limit)
            {
                outputStr += tempStr;
            }
            else
            {
                break;
            }
        }
        return outputStr;
    }
    #endregion
}
