using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class BattleStateCondition_HitPoint : BattleStateCondition
    {
        private RatioValueType _ratioValueType;
        private ComparisonType _comparisonType;
        private float _standardHitPoint;

        public BattleStateCondition_HitPoint(BattleStateConditionProfile profile, BattleActor actor, BattleState state)
            : base(profile, actor, state)
        {
            _ratioValueType = profile.Params.GetRatioValue("RatioValueType").Value;
            _comparisonType = profile.Params.GetComparison("ComparisonType").Value;
            _standardHitPoint = profile.Params.GetFloat("StandardHitPoint").Value;
        }

        public override bool IsInCondition()
        {
            var profile = _profile;
            float compare;

            if (_ratioValueType == RatioValueType.Ratio)
            {
                compare = _actor.HitPoint / _actor.HitPointMax;
            }
            else
            {
                compare = _actor.HitPoint;
            }

            if (_comparisonType.Has(ComparisonType.Equal))
            {
                if (compare == _standardHitPoint)
                    return true;
            }

            if (_comparisonType.Has(ComparisonType.Less))
                return compare < _standardHitPoint;
            else
                return compare > _standardHitPoint;
        }
    }
}