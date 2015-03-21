using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BattleCondition_HitPoint : BattleCondition<BattleConditionProfile_HitPoint>
{
    public override bool IsInCondition()
    {
        var profile = Profile;
        float compare;
        
        if (profile.RatioValueType == Condition_RatioValueType.Ratio)
        {
            compare = _actor.HitPoint / _actor.HitPointMax;
        }
        else
        {
            compare = _actor.HitPoint;   
        }

        if (profile.ComparisonType.Has(Condition_ComparisonType.Equal))
        {
            if (compare == profile.StandardHitPoint)
                return true;
        }

        if (profile.ComparisonType.Has(Condition_ComparisonType.Less))
            return compare < profile.StandardHitPoint;
        else
            return compare > profile.StandardHitPoint;
    }
}