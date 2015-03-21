using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public static class CollectionExtension
{
    public static bool ContainsProperly<T>(this List<T> collection, T item, IComparer<T> comparer)
    {
        if (collection.Count > 10000)
        {
            //Debug.Log(collection.Count);
            return collection.BinarySearch(item, comparer) > 0;
        }

        return collection.Contains(item);
    }
}
