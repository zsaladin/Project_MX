using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    [System.Serializable]
    public class ProjectileProfile : Profile
    {
        public GameObject Prefab;
        public ProjectileMotionType ProjectileMotionType;
        public float Speed;
        public float MaxHeight;
    }
}
