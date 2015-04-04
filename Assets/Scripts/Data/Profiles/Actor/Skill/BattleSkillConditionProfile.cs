using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    [System.Serializable]
    public class BattleSkillConditionProfile : Profile
    {
        public SkillConditionType Type;
        public ParamData Params = new ParamData();
    }
}
