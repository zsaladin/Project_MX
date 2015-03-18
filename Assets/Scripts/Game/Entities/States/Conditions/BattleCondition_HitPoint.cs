using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BattleCondition_HitPoint : BattleCondition<BattleConditionProfile_HitPoint>
{
    public override bool IsInCondition()
    {
        var profile = Profile;
        
        if (profile.RatioValueType == Condition_RatioValueType.Ratio)
        {

        }
        else
        {
            
        }
        return true;
    }
}