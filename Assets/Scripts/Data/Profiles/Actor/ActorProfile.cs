using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class ActorProfile : Profile 
{
    public GameObject Prefab;
    public int ActorType;

    public float Size;
    public float HitPointMax;

    public OffenseType OffenseType;
    public float OffensePower;
    public float OffenseTime;
    public float OffenseDealTime;
    public float OffenseRange;

    public DefenseType DefenseType;

    public float MovingSpeed;
}
