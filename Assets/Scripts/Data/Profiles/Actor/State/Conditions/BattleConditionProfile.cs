using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class BattleConditionProfile : Profile 
{
    public ConditionType Type;

    public static BattleConditionProfile Create(ConditionType type)
    {
        BattleConditionProfile condition;
        switch(type)
        {
            case ConditionType.HitPoint:
                condition = new BattleConditionProfile_HitPoint();
                break;
            case ConditionType.IsTargetInOffenseRange:
                condition = new BattleConditionProfile_IsTargetInOffenseRange();
                break;
            default:
                return null;
        }

        condition.Type = type;
        return condition;
    }

#if UNITY_EDITOR
    public virtual void DrawConditionInEditor()
    {

    }
#endif
}
