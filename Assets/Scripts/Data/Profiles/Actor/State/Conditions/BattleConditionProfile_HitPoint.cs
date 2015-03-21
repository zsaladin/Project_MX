using UnityEngine;
using System.Collections.Generic;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

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
        Type = ConditionType.HitPoint;
    }

#if UNITY_EDITOR

    public override void DrawConditionInEditor()
    {
        StandardHitPoint = EditorGUILayout.FloatField(StandardHitPoint, GUILayout.Width(150));
        RatioValueType = (Condition_RatioValueType)EditorGUILayout.EnumPopup(RatioValueType, GUILayout.Width(150));
        ComparisonType = (Condition_ComparisonType)EditorGUILayout.EnumMaskField(ComparisonType, GUILayout.Width(150));
    }
#endif
}
