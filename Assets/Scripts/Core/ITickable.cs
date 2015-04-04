using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public interface ITickable
    {
        void OnTick();
    }
}
