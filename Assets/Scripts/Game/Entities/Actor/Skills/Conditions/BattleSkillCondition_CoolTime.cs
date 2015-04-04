using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class BattleSkillCondition_CoolTime : BattleSkillCondition
    {
        private float _coolTime;
        private float _currentCoolTime;

        public BattleSkillCondition_CoolTime(BattleSkillConditionProfile profile, BattleActor actor, BattleSkill skill)
            : base(profile, actor, skill)
        {
            _coolTime = profile.Params.GetFloat("CoolTime").Value;
            _currentCoolTime = 0f;
        }

        public override void OnSkill()
        {
            base.OnSkill();

            _currentCoolTime = _coolTime;
        }

        public override void OnTick()
        {
            base.OnTick();

            _currentCoolTime -= Manager.Constant.GAME_TICK;
        }

        public override bool IsInCondition()
        {
            return _currentCoolTime <= 0f;
        }
    }
}