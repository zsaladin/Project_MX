using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BattleAction_Defense : BattleAction
{
    public override ActionType Type
    {
        get
        {
            return ActionType.Defense;
        }
    }

    public override void OnBegin()
    {
        
    }

    public override void OnTick()
    {
        //Debug.Log(string.Format("{0} : {1}", Actor.name, GetType().Name));
    }

    public override void OnEnd()
    {
        
    }

    public void Defense(BattleAction_Offense attackAction)
    {
        Actor.HitPoint -= attackAction.Actor.OffensePower;
    }
}

public enum DefenseType
{
    Invalid     = 0x00,
    Light       = 0x01,
    Medium      = 0x02,
    Heavy       = 0x04,
}