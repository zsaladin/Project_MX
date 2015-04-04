using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public abstract class BattleBuffCondition
    {
        private BattleBuffConditionProfile _profile;
        private BattleActor _actor;

        public BattleBuffCondition(BattleBuffConditionProfile profile, BattleActor actor)
        {
            _profile = profile;
            _actor = actor;
        }

        public virtual void OnBegin() { }
        public virtual void OnTick() { }
        public virtual void OnEnd() { }
        public abstract bool IsInCondition();

        public static BattleBuffCondition Create(BattleBuffConditionProfile profile, BattleActor actor)
        {
            switch(profile.Type)
            {
                case BuffConditionType.Duration:
                    return new BattleBuffCondition_Duration(profile, actor);
            }

            return null;
        }
    }

    public enum BuffConditionType
    {
        Invalid,
        Duration,
    }

}