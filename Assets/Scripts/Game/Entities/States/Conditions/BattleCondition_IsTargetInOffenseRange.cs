using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BattleCondition_IsTargetInRange : BattleCondition<BattleConditionProfile_IsTargetInOffenseRange> 
{
    public override bool IsInCondition()
    {
        if (_state.Target == null) return false;

        // It must be chagned from 'maginitude' to  'sqrMaginitude'
        Vector3 diff = _state.Target.transform.position - _actor.transform.position;
        return diff.magnitude <= _actor.OffenseRange - (_actor.Size * 0.5f) - (_state.Target.Size * 0.5f);
    }
}
