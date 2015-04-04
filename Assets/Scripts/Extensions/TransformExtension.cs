using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public static class TransformExtension
    {
        public static Transform FindChildRecursively(this Transform transfrom, string name)
        {
            Transform[] trans = transfrom.GetComponentsInChildren<Transform>();
            for (int i = 0; i < trans.Length; ++i)
            {
                if (trans[i].name == name)
                    return trans[i];
            }

            return null;
        }
    }
}
