using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public abstract class BattleSkillCondition : ITickable
    {
        protected BattleSkillConditionProfile _profile;
        protected BattleActor _actor;
        protected BattleSkill _skill;


        public BattleSkillCondition(BattleSkillConditionProfile profile, BattleActor actor, BattleSkill skill)
        {
            _profile = profile;
            _actor = actor;
            _skill = skill;
        }

        public abstract bool IsInCondition();
        public virtual void OnSkill() { }
        public virtual void OnTick() { }

        public static BattleSkillCondition Create(BattleSkillConditionProfile profile, BattleActor actor, BattleSkill skill)
        {
            switch (profile.Type)
            {
                case SkillConditionType.CoolTime:
                    return new BattleSkillCondition_CoolTime(profile, actor, skill);
                case SkillConditionType.Range:
                    return new BattleSkillCondition_Range(profile, actor, skill);
            }

            return null;
        }
    }

    public enum SkillConditionType
    {
        Invalid,
        CoolTime,
        Range,
    }
}