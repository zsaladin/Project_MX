using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class EnumExtension 
{
    public static bool Has<T>(this T enumFirst, T enumSecond) where T : struct
    {
        if (typeof(T).IsEnum == false) return false;

        int first = System.Convert.ToInt32(enumFirst);
        int second = System.Convert.ToInt32(enumSecond);

        return (first & second) == second;
    }
}
