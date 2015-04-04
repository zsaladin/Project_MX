using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    [System.Serializable]
    public class BattleStateConditionProfile : Profile
    {
        public StateConditionType Type;
        public ParamData Params = new ParamData();

#if UNITY_EDITOR
        public virtual void DrawConditionInEditor()
        {

        }
#endif
    }
}