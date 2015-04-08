using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public abstract class BattleBuffAction
    {
        protected BattleActor _actor;
        protected BattleActor _caster;
        protected BattleBuffActionProfile _profile;

        public abstract BuffActionType Type { get; }

        public BattleBuffAction(BattleBuffActionProfile profile, BattleActor actor, BattleActor caster)
        {
            _actor = actor;
            _caster = caster;
            _profile = profile;
        }

        public virtual void OnBegin() { }
        public virtual void OnTick() { }
        public virtual void OnEnd() { }
        public virtual void Update() { }

        public static BattleBuffAction Create(BattleBuffActionProfile profile, BattleActor actor, BattleActor caster)
        {
            switch(profile.Type)
            {
                case BuffActionType.Interruption:
                    return new BattleBuffAction_Interruption(profile, actor, caster);
                case BuffActionType.Airborn:
                    return new BattleBuffAction_Airborn(profile, actor, caster);
                case BuffActionType.Knockback:
                    return new BattleBuffAction_Knockback(profile, actor, caster);
                case BuffActionType.MovementSpeed:
                    return new BattleBuffAction_MovementSpeed(profile, actor, caster);
                case BuffActionType.DamageOverTime:
                    return new BattleBuffAction_DamageOverTime(profile, actor, caster);
            }

            return null;
        }
    }

    public enum BuffActionType
    {
        Invalid,
        Interruption,
        Airborn,
        Knockback,
        MovementSpeed,
        DamageOverTime,
    }
}
