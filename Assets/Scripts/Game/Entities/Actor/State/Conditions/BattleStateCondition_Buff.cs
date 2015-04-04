using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class BattleStateCondition_Buff : BattleStateCondition
    {
        private BuffActionType _buffActionType;
        private bool _isTrue;

        public BattleStateCondition_Buff(BattleStateConditionProfile profile, BattleActor actor, BattleState state)
            : base(profile, actor, state)
        {
            _buffActionType = _profile.Params.GetBuffActionTypeValue("BuffActionType").Value;
            _isTrue = _profile.Params.GetBool("IsTrue").Value;
        }

        public override bool IsInCondition()
        {
            bool contains = _actor.BuffMachine.ContainsBuff(_buffActionType);
            return _isTrue ? contains : !contains;
        }
    }
}