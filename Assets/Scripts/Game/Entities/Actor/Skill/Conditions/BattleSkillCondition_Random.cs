using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class BattleSkillCondition_Random : BattleSkillCondition
    {
        private float _ratio;
        public BattleSkillCondition_Random(BattleSkillConditionProfile profile, BattleActor actor, BattleSkill skill)
            : base(profile, actor, skill)
        {
            _ratio = profile.Params.GetFloat("Ratio").Value;
        }

        public override bool IsInCondition()
        {
            return Random.value <= _ratio;
        }
    }
}