using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public abstract class BattleStateCondition
    {
        protected BattleActor _actor;
        protected BattleState _state;

        protected BattleStateConditionProfile _profile;


        public BattleStateCondition(BattleStateConditionProfile profile, BattleActor actor, BattleState state)
        {
            _profile = profile;
            _actor = actor;
            _state = state;
        }

        public abstract bool IsInCondition();

        public static BattleStateCondition Create(BattleStateConditionProfile profile, BattleActor actor, BattleState state)
        {
            switch (profile.Type)
            {
                case StateConditionType.HitPoint:
                    return new BattleStateCondition_HitPoint(profile, actor, state);
                case StateConditionType.IsTargetInOffenseRange:
                    return new BattleStateCondition_IsTargetInRange(profile, actor, state);
                case StateConditionType.IsActionEnded:
                    return new BattleStateCondition_IsActionEnded(profile, actor, state);
                case StateConditionType.ExistsTarget:
                    return new BattleStateCondition_ExistsTarget(profile, actor, state);
                case StateConditionType.IsSkillAvailable:
                    return new BattleStateCondition_IsSkillAvailable(profile, actor, state);
            }

            return null;
        }
    }

    //public abstract class BattleCondition<TProfile> : BattleCondition
    //    where TProfile : BattleConditionProfile
    //{
    //    public new TProfile Profile { get; protected set; }
    //    public override void Init(BattleConditionProfile profile, BattleActor actor, BattleState state)
    //    {
    //        base.Init(profile, actor, state);
    //        Profile = profile as TProfile;
    //    }
    //}

    public enum RatioValueType
    {
        Ratio,
        Value
    }

    public enum ComparisonType
    {
        Equal = 0x01,
        More = 0x02,
        Less = 0x04,
    }

    public enum StateConditionType
    {
        Invalid,
        HitPoint,
        IsTargetInOffenseRange,
        IsActionEnded,
        ExistsTarget,
        IsSkillAvailable,
    }
}