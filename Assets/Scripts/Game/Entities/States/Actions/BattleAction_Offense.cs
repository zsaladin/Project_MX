using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BattleAction_Offense : BattleAction
{
    protected BattleActor _target;

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
        base.OnBegin();

        _currentDuratiion = 0;
        _isDealed = false;

        Animation animation = Actor.GetComponent<Animation>();
        animation.Stop();
        if (animation.GetClip("Attack_01"))
            animation.Play("Attack_01");
        else
            animation.Play("attack");

        _target = Actor.Target;
        if (_target != null)
            Actor.transform.LookAt(_target.transform);
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
            if (_isDealed == false)
            {
                _isDealed = true;
                Offense();
            }
        }
    }

    public override void OnEnd()
    {
        
    }

    public override void Update()
    {
        
    }

    void Offense()
    {
        if (_target == null) return;

        var defenseAction = _target.CurrentState.Actions.Find(item => item is BattleAction_Defense) as BattleAction_Defense;
        if (defenseAction != null)
            defenseAction.Defense(this);
    }
}


public enum OffenseType
{
    Invalid     = 0x00,
    Melee       = 0x01,
    Range       = 0x02,
    Magic       = 0x04,
}