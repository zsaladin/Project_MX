using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class BattleStateCondition_ExistsTarget : BattleStateCondition
    {
        private bool _isTrue;
        public BattleStateCondition_ExistsTarget(BattleStateConditionProfile profile, BattleActor actor, BattleState state)
            : base(profile, actor, state)
        {
            _isTrue = profile.Params.GetBool("IsTrue").Value;
        }

        public override bool IsInCondition()
        {
            if (_isTrue)
                return _actor.Target != null;
            else
                return _actor.Target == null;
        }
    }
}