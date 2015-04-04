using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public interface IProjectileReached
    {
        void OnProjectileReached(Vector3 targetPosition);
    }
}