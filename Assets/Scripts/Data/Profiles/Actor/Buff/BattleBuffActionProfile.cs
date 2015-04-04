using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    [System.Serializable]
    public class BattleBuffActionProfile : Profile
    {
        public BuffActionType Type;
        public ParamData Params = new ParamData();
    }
}