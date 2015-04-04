using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class BattleStateCondition_IsSkillAvailable : BattleStateCondition
    {
        private List<BattleSkill> _availableSkills = new List<BattleSkill>();
        public BattleStateCondition_IsSkillAvailable(BattleStateConditionProfile profile, BattleActor actor, BattleState state)
            : base(profile, actor, state)
        {

        }

        public override bool IsInCondition()
        {
            _availableSkills.Clear();
            for (int i = 0; i < _actor.SkillMachine.Skills.Length; ++i)
            {
                if (_actor.SkillMachine.Skills[i].IsInCondition())
                {
                    _availableSkills.Add(_actor.SkillMachine.Skills[i]);
                }
            }

            if (_availableSkills.Count > 0)
            {
                _actor.SkillMachine.NextSkill = _availableSkills[Random.Range(0, _availableSkills.Count)];
                return true;
            }

            return false;
        }
    }
}