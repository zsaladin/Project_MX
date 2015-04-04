using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class BattleBuffAction_Knockback : BattleBuffAction
    {
        private float _farFrom;
        private float _duration;
        private float _distance;
        private float _velocity;
        private float _currentDuration;

        private Vector3 _direction;

        public override BuffActionType Type
        {
            get { return BuffActionType.Knockback; }
        }

        public BattleBuffAction_Knockback(BattleBuffActionProfile profile, BattleActor actor, BattleActor attacker)
            : base(profile, actor, attacker)
        {
            _duration = profile.Params.GetFloat("Duration").Value;
            _distance = profile.Params.GetFloat("Distance").Value;
            _farFrom = profile.Params.GetFloat("FarFrom").Value;
        }

        public override void OnBegin()
        {
            base.OnBegin();

            _currentDuration = 0;
            _velocity = _distance / _duration;
            //_direction = (_actor.Position - _attacker.Position).normalized;

            Vector3 startPosition = _attacker.Position + _attacker.BaseAction.Direction * _farFrom;
            _direction = (_actor.Position - startPosition).normalized;
        }

        public override void OnTick()
        {
            base.OnTick();
            _currentDuration += Manager.Constant.GAME_TICK;
            if (_currentDuration <= _duration)
                _actor.Position += _direction * _velocity * Manager.Constant.GAME_TICK;
        }

        public override void Update()
        {
            base.Update();
            _actor.transform.position = Vector3.MoveTowards(_actor.transform.position, _actor.Position, _velocity * Time.deltaTime);
        }
    }
}