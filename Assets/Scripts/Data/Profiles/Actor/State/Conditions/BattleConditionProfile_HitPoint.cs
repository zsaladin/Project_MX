using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class BattleConditionProfile_HitPoint : BattleConditionProfile 
{
    [SerializeField]
    public float StandardHitPoint;
    [SerializeField]
    public Condition_RatioValueType RatioValueType;
    [SerializeField]
    public Condition_ComparisonType ComparisonType;

    public BattleConditionProfile_HitPoint()
    {
        Name = "HitPoint";
    }

#if UNITY_EDITOR
    public override void DrawConditionInEditor()
    {
        
    }
#endif
}
