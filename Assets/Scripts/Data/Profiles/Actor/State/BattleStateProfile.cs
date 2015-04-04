using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    [System.Serializable]
    public class BattleStateProfile : Profile
    {
        public List<ConditionStateSet> ConditionStateSet = new List<ConditionStateSet>();
        public List<ActionType> Actions = new List<ActionType>();
        public bool IsDefault = false;
    }

    [System.Serializable]
    public class ConditionStateSet
    {
        public BattleStateConditionProfile ConditionProfile = new BattleStateConditionProfile();
        public int StateProfileID;
    }
}