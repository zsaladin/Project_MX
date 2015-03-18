using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class EnumExtension 
{
    public static bool Has<T>(this T enumFirst, T enumSecond) where T : struct, System.IFormattable, System.IConvertible, System.IComparable
    {
        if (typeof(T).IsEnum == false) return false;

        long first = System.Convert.ToInt64(enumFirst);
        long second = System.Convert.ToInt64(enumSecond);

        return (first & second) == second;
    }
}
