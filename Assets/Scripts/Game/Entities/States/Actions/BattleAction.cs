using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public abstract class BattleAction : ITickable 
{
    public BattleActor Actor { get; set; }

    public bool IsEnd { get; protected set; }
    public bool ForceEnd { get; set; }

    public virtual ActionType Type { get { return ActionType.Invalid; }}

    public virtual void OnBegin() { }
    public virtual void OnTick() { }
    public virtual void OnEnd() { }
    public virtual void Update() { }

    public static BattleAction Create(ActionType type)
    {
        switch(type)
        {
            case ActionType.Offense:
                return new BattleAction_Offense();
            case ActionType.Defense:
                return new BattleAction_Defense();
            case ActionType.Movement:
                return new BattleAction_Movement();
            case ActionType.SearchForTarget:
                return new BattleAction_SearchForTarget();
        }

        return null;
    }
}


public enum ActionType
{
    Invalid = 0,
    Offense,
    Defense,
    Movement,
    SearchForTarget,
}