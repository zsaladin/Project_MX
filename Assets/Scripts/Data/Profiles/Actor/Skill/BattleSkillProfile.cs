using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    [System.Serializable]
    public class BattleSkillProfile : Profile
    {
        public SkillType Type;
        public ParamData Params = new ParamData();
        public List<BattleSkillConditionProfile> Conditions = new List<BattleSkillConditionProfile>();
    }
}
