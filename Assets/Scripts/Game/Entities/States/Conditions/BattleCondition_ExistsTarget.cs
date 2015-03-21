using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BattleCondition_ExistsTarget : BattleCondition<BattleConditionProfile_ExistsTarget> 
{
    public override bool IsInCondition()
    {
        if (Profile.IsTrue)
            return _actor.Target != null;
        else
            return _actor.Target == null;
    }
}
