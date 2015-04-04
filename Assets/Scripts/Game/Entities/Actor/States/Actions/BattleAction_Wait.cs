using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class BattleAction_Wait : BattleAction
    {
        public BattleAction_Wait(BattleActor actor)
            : base(actor)
        {

        }

        public override ActionType Type
        {
            get
            {
                return ActionType.Wait;
            }
        }

        public override void OnBegin()
        {
            base.OnBegin();

            Actor.AnimationController.Play(AnimationType.Idle);
        }
    }
}