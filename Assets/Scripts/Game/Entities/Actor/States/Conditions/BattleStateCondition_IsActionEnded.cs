using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class BattleStateCondition_IsActionEnded : BattleStateCondition
    {
        private BattleAction _action;

        public BattleStateCondition_IsActionEnded(BattleStateConditionProfile profile, BattleActor actor, BattleState state)
            : base(profile, actor, state)
        {
            ActionType actionType = profile.Params.GetActionTypeValue("ActionType").Value;
            _action = _state.FindAction(actionType);
        }

        public override bool IsInCondition()
        {
            if (_action == null) return false;
            return _action.IsEnd;
        }
    }
}