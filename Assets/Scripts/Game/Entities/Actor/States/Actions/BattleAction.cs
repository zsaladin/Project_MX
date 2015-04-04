using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public abstract class BattleAction : ITickable
    {
        public BattleActor Actor { get; private set; }

        public bool IsEnd { get; protected set; }
        public bool ForceEnd { get; set; }

        public virtual ActionType Type { get { return ActionType.Invalid; } }

        public BattleAction(BattleActor actor)
        {
            Actor = actor;
        }

        public virtual void OnBegin()
        {
            IsEnd = false;
            ForceEnd = false;
        }

        public virtual void OnTick() { }
        public virtual void OnEnd() { }
        public virtual void Update() { }

        public void LookAt(Vector3 targetPosition)
        {

        }

        public static BattleAction Create(ActionType type, BattleActor actor)
        {
            switch (type)
            {
                case ActionType.Offense:
                    return new BattleAction_Offense(actor);
                case ActionType.Defense:
                    return new BattleAction_Defense(actor);
                case ActionType.Movement:
                    return new BattleAction_Movement(actor);
                case ActionType.SearchForTarget:
                    return new BattleAction_SearchForTarget(actor);
                case ActionType.Death:
                    return new BattleAction_Death(actor);
                case ActionType.Wait:
                    return new BattleAction_Wait(actor);
                case ActionType.Skill:
                    return new BattleAction_Skill(actor);
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
        Death,
        Wait,
        Skill
    }
}