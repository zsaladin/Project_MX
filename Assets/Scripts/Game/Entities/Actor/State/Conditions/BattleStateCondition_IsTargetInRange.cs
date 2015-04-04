using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class BattleStateCondition_IsTargetInRange : BattleStateCondition
    {
        private bool _isTrue;
        public BattleStateCondition_IsTargetInRange(BattleStateConditionProfile profile, BattleActor actor, BattleState state)
            : base(profile, actor, state)
        {
            _isTrue = profile.Params.GetBool("IsTrue").Value;
        }

        public override bool IsInCondition()
        {
            if (_actor.Target == null)
                return _isTrue == false;

            // It must be chagned from 'maginitude' to  'sqrMaginitude'
            Vector3 diff = _actor.Target.Position - _actor.Position;
            bool isInRange = diff.magnitude <= _actor.OffenseRange + (_actor.Size * 0.5f) + (_actor.Target.Size * 0.5f);
            if (_isTrue)
                return isInRange;
            else
                return !isInRange;
        }
    }
}