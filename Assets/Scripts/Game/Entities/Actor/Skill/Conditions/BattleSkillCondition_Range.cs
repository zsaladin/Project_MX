using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class BattleSkillCondition_Range : BattleSkillCondition
    {
        private ComparisonType _comparisonType;
        private float _range;

        public BattleSkillCondition_Range(BattleSkillConditionProfile profile, BattleActor actor, BattleSkill skill)
            : base(profile, actor, skill)
        {
            _comparisonType = profile.Params.GetComparison("ComparisonType").Value;
            _range = profile.Params.GetFloat("Range").Value;
        }

        public override bool IsInCondition()
        {
            if (_actor.Target == null) return false;

            float sqrMag = (_actor.Target.Position - _actor.Position).sqrMagnitude;
            float sqrRange = _range * _range;
            if (_comparisonType.Has(ComparisonType.Equal))
            {
                if (sqrMag == sqrRange)
                    return true;
            }

            if (_comparisonType.Has(ComparisonType.Less))
            {
                if (sqrMag < sqrRange)
                    return true;
            }

            if (_comparisonType.Has(ComparisonType.More))
            {
                if (sqrMag  > sqrRange)
                    return true;
            }

            return false;
        }
    }
}