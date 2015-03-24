using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class ProjectileProfile : Profile 
{
    public GameObject Prefab;
    public ProjectileMotionType ProjectileMotionType;
    public float Speed;
    public float MaxHeight;
}
