using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class BattleStateProfile : Profile
{
    public List<ConditionStateSet> ConditionStateSet = new List<ConditionStateSet>();
    public List<ActionType> Actions = new List<ActionType>();
}

[System.Serializable]
public class ConditionStateSet
{
    public ConditionType ConditionType;
    public int ConditionProfileID;
    public int StateProfileID;
}