using UnityEngine;
using System.Collections.Generic;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif 

[System.Serializable]
public class BattleConditionProfile_IsTargetInOffenseRange : BattleConditionProfile
{
    [SerializeField]
    public bool IsTrue;

    public BattleConditionProfile_IsTargetInOffenseRange()
    {
        Name = "IsTargetInOffenseRange";
    }

#if UNITY_EDITOR
    public override void DrawConditionInEditor()
    {
        IsTrue = GUILayout.Toggle(IsTrue, "IsTrue", GUILayout.Width(150));
    }
#endif
}
