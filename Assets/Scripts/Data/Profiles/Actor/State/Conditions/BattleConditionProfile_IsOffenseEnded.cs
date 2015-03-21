using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class BattleConditionProfile_IsOffenseEnded : BattleConditionProfile 
{
    public BattleConditionProfile_IsOffenseEnded()
    {
        Name = "IsOffenseEnded";
        Type = ConditionType.IsOffenseEnded;
    }
}
