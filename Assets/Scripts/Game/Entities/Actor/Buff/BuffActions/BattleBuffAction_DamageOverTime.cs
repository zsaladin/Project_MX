using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class BattleBuffAction_DamageOverTime : BattleBuffAction
    {
        private float _tickDuration;
        private float _damage;
        private float _radius;

        private float _currentTickDuration = 0;

        public override BuffActionType Type
        {
            get { return BuffActionType.DamageOverTime; }
        }
        public BattleBuffAction_DamageOverTime(BattleBuffActionProfile profile, BattleActor actor, BattleActor caster)
            : base(profile, actor, caster)
        {
            _damage = profile.Params.GetFloat("Damage").Value;
            _tickDuration = profile.Params.GetFloat("TickDuration").Value;
            _radius = profile.Params.GetFloat("Radius").Value;
        }

        public override void OnBegin()
        {
            base.OnBegin();
            _currentTickDuration = _tickDuration;
        }

        public override void OnTick()
        {
            base.OnTick();

            _currentTickDuration -= Manager.Constant.GAME_TICK;
            if (_currentTickDuration > 0) return;

            _currentTickDuration += _tickDuration;

            var opponentActors = Manager.Entity.GetActors(_actor.OpponentOwnerShip, false);
            for(int i = 0; i < opponentActors.Count; ++i)
            {
                BattleActor target = opponentActors[i];
                BattleAction_Defense defenseAction = target.StateMachine.CurrentState.FindAction<BattleAction_Defense>();
                if (defenseAction == null) continue;

                float sqrDistance = (target.Position - _actor.Position).sqrMagnitude;
                if (sqrDistance > _radius * _radius) continue;

                defenseAction.Defense(_damage);
            }

        }

    }
}