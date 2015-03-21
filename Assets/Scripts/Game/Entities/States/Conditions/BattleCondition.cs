using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public abstract class BattleCondition 
{
    protected BattleActor _actor;
    protected BattleState _state;
    protected BattleConditionProfile _profile;
    public BattleConditionProfile GetProfile() { return _profile; }

    public virtual void Init(BattleConditionProfile profile, BattleActor actor, BattleState state) 
    {
        _profile = profile; 
        _actor = actor;
        _state = state;
    }

    public abstract bool IsInCondition();

    public static BattleCondition Create(ConditionType type)
    {
        switch(type)
        {
            case ConditionType.HitPoint:
                return new BattleCondition_HitPoint();
            case ConditionType.IsTargetInOffenseRange:
                return new BattleCondition_IsTargetInRange();
            case ConditionType.IsOffenseEnded:
                return new BattleCondition_IsOffenseEnded();
            case ConditionType.ExistsTarget:
                return new BattleCondition_ExistsTarget();
        }

        return null;
    }
}

public abstract class BattleCondition<TProfile> : BattleCondition
    where TProfile : BattleConditionProfile
{
    public TProfile Profile { get; protected set; }
    public override void Init(BattleConditionProfile profile, BattleActor actor, BattleState state)
    {
        base.Init(profile, actor, state);
        Profile = profile as TProfile;
    }
}

public enum Condition_RatioValueType
{
    Ratio,
    Value
}

public enum Condition_ComparisonType
{
    Equal       = 0x01,
    More        = 0x02,
    Less        = 0x04,
}

public enum ConditionType
{
    Invalid,
    HitPoint,
    IsTargetInOffenseRange,
    IsOffenseEnded,
    ExistsTarget,
}