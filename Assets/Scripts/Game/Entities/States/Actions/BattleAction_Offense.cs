using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BattleAction_Offense : BattleAction
{
    protected BattleAction_Defense _target;

    protected float _currentDuratiion;
    protected bool _isDealed;
    
    public override ActionType Type
    {
        get
        {
            return ActionType.Offense;
        }
    }

    public override void OnBegin()
    {
        _currentDuratiion = 0;
        _isDealed = false;
    }
    
    public override void OnTick()
    {
        _currentDuratiion += Manager.Constant.GAME_TICK;

        if (_currentDuratiion > Actor.OffenseTime)
        {
            IsEnd = true;
            return;
        }

        if (_currentDuratiion >= Actor.OffenseDealTime)
        {
            if (_isDealed)
            {
                _isDealed = true;
                Offense();
            }
        }
    }

    public override void OnEnd()
    {
        
    }

    void Offense()
    {
        _target.Defense(this);
    }
}


public enum OffenseType
{
    Invalid     = 0x00,
    Melee       = 0x01,
    Range       = 0x02,
    Magic       = 0x04,
}