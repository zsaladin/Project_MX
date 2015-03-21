using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ConditionProfileSave : ScriptableObject
{
    public List<BattleConditionProfile_HitPoint> Conditions_HitPoint = new List<BattleConditionProfile_HitPoint>();
    public List<BattleConditionProfile_IsTargetInOffenseRange> Conditions_IsTargetInOffenseRange = new List<BattleConditionProfile_IsTargetInOffenseRange>();
    public List<BattleConditionProfile_IsOffenseEnded> Conditions_IsOffenseEnded = new List<BattleConditionProfile_IsOffenseEnded>();
    public List<BattleConditionProfile_ExistsTarget> Conditions_ExistsTarget = new List<BattleConditionProfile_ExistsTarget>();

    public BattleConditionProfile GetConditionProfile(ConditionType type, int id)
    {
        switch (type)
        {
            case ConditionType.HitPoint:
                return Conditions_HitPoint.Find(item => item.ID == id);
            case ConditionType.IsTargetInOffenseRange:
                return Conditions_IsTargetInOffenseRange.Find(item => item.ID == id);
            case ConditionType.IsOffenseEnded:
                return Conditions_IsOffenseEnded.Find(item => item.ID == id);
            case ConditionType.ExistsTarget:
                return Conditions_ExistsTarget.Find(item => item.ID == id);
        }

        return null;
    }
}
