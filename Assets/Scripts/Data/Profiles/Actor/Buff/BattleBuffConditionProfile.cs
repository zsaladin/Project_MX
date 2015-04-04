using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    [System.Serializable]
    public class BattleBuffConditionProfile : Profile
    {
        public BuffConditionType Type;
        public ParamData Params = new ParamData();
    }
}