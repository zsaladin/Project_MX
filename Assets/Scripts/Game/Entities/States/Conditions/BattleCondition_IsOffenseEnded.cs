using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BattleCondition_IsOffenseEnded : BattleCondition<BattleConditionProfile_IsOffenseEnded> 
{
    private BattleAction_Offense _offenseAction;
    private bool _isInit = false;
    public override bool IsInCondition()
    {
        if (_isInit == false)
        {
            _offenseAction = _actor.CurrentState.Actions.Find(item => item is BattleAction_Offense) as BattleAction_Offense;
            _isInit = true;
        }

        if (_offenseAction == null) return true;
        return _offenseAction.IsEnd;
    }
}
