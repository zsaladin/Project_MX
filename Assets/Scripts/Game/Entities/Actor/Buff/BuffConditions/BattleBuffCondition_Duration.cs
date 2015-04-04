using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class BattleBuffCondition_Duration : BattleBuffCondition
    {
        private float _duration;
        private float _currentDuration;

        public BattleBuffCondition_Duration(BattleBuffConditionProfile profile, BattleActor actor)
            : base(profile, actor)
        {
            _duration = profile.Params.GetFloat("Duration").Value;
        }

        public override void OnBegin()
        {
            base.OnBegin();
            _currentDuration = _duration;
        }

        public override void OnTick()
        {
            base.OnTick();
            _currentDuration -= Manager.Constant.GAME_TICK;
        }

        public override bool IsInCondition()
        {
            return _currentDuration <= 0;    
        }
    }
}