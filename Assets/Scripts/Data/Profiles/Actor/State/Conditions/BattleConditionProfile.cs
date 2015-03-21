using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class BattleConditionProfile : Profile 
{
    public ConditionType Type;

#if UNITY_EDITOR
    public virtual void DrawConditionInEditor()
    {

    }
#endif
}
