using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class BattleAction_Death : BattleAction
    {
        public override ActionType Type
        {
            get
            {
                return ActionType.Death;
            }
        }

        public BattleAction_Death(BattleActor actor)
            : base(actor)
        {

        }

        public override void OnBegin()
        {
            base.OnBegin();
            Manager.Entity.RemoveActor(Actor);

            Actor.AnimationController.Play(AnimationType.Die);
        }

        public override void Update()
        {
            base.Update();

            if (Actor.AnimationController.IsPlaying(AnimationType.Die) == false)
            {
                GameObject.Destroy(Actor.gameObject);
            }
        }
    }
}