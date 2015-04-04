using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class BattleStateCondition_IsSkillAvailable : BattleStateCondition
    {
        public BattleStateCondition_IsSkillAvailable(BattleStateConditionProfile profile, BattleActor actor, BattleState state)
            : base(profile, actor, state)
        {

        }

        public override bool IsInCondition()
        {
            for (int i = 0; i < _actor.SkillMachine.Skills.Length; ++i)
            {
                if (_actor.SkillMachine.Skills[i].IsInCondition())
                    return true;
            }

            return false;
        }
    }
}