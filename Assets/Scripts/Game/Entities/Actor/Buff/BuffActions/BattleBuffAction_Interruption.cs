using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class BattleBuffAction_Interruption : BattleBuffAction
    {
        public override BuffActionType Type
        {
            get { return BuffActionType.Interruption; }
        }

        public BattleBuffAction_Interruption(BattleBuffActionProfile profile, BattleActor actor)
            : base(profile, actor)
        {

        }

        public override void OnBegin()
        {
            base.OnBegin();
            _actor.AnimationController.Stop();

        }

        public override void OnTick()
        {
            base.OnTick();
        }

        public override void OnEnd()
        {
            base.OnEnd();
            _actor.AnimationController.Play();
        }
    }
}
