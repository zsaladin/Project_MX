using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class BattleSkillCondition_Range : BattleSkillCondition
    {
        private float _range;

        public BattleSkillCondition_Range(BattleSkillConditionProfile profile, BattleActor actor, BattleSkill skill)
            : base(profile, actor, skill)
        {
            _range = profile.Params.GetFloat("Range").Value;
        }

        public override bool IsInCondition()
        {
            if (_actor.Target == null) return false;

            return ((_actor.Target.Position - _actor.Position).sqrMagnitude) <= _range * _range;
        }
    }
}