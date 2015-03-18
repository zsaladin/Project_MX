using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ConditionProfileSave : ScriptableObject
{
    public List<BattleConditionProfile_HitPoint> Conditions_HitPoint = new List<BattleConditionProfile_HitPoint>();
    public List<BattleConditionProfile_IsTargetInOffenseRange> Conditions_IsTargetInOffenseRange = new List<BattleConditionProfile_IsTargetInOffenseRange>();

    public BattleConditionProfile GetConditionProfile(ConditionType type, int id)
    {
        switch (type)
        {
            case ConditionType.HitPoint:
                return Conditions_HitPoint.Find(item => item.ID == id);
            case ConditionType.IsTargetInOffenseRange:
                return Conditions_IsTargetInOffenseRange.Find(item => item.ID == id);
        }

        return null;
    }
}
