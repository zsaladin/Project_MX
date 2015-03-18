using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BattleAction_Movement : BattleAction
{
    public override ActionType Type
    {
        get
        {
            return ActionType.Movement;
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
}


public enum MovementType
{
    Invalid     = 0x00,
    Normal      = 0x01,
    Flying      = 0x02,
}