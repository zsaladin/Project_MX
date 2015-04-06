using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class BattleBuffAction_Airborn : BattleBuffAction
    {
        private float _duration;
        private float _currentDuration;

        private float _currentDeltaDuration;

        private Vector3 _firstPosition;
        private float _firstVelocity;
        private const float _gravity = -20f;

        public override BuffActionType Type
        {
            get { return BuffActionType.Airborn; }
        }

        public BattleBuffAction_Airborn(BattleBuffActionProfile profile, BattleActor actor, BattleActor caster)
            : base(profile, actor, caster)
        {
            _duration = profile.Params.GetFloat("Duration").Value;
        }

        public override void OnBegin()
        {
            base.OnBegin();
            _currentDuration = 0;
            _currentDeltaDuration = 0;
            _firstPosition = _actor.Position;

            _firstVelocity = -0.5f * _gravity * _duration;
        }

        public override void OnTick()
        {
            base.OnTick();
            _currentDuration += Manager.Constant.GAME_TICK;
            if (_currentDuration > _duration) _currentDuration = _duration;

            float currentHeight = _firstVelocity * _currentDuration +  0.5f * _gravity * _currentDuration * _currentDuration;
            Vector3 currentPosition = _firstPosition;
            currentPosition.y = currentHeight;

            _actor.Position = currentPosition;
        }

        public override void Update()
        {
            base.Update();
            _currentDeltaDuration += Time.deltaTime;
            float currentVeclocity = Mathf.Abs(_gravity * _currentDeltaDuration + _firstVelocity);
            _actor.transform.position = Vector3.MoveTowards(_actor.transform.position, _actor.Position,  currentVeclocity * Time.deltaTime);
        }
    }
}