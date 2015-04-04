﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public abstract class BattleBuffAction
    {
        protected BattleActor _actor;
        protected BattleBuffActionProfile _profile;

        public abstract BuffActionType Type { get; }

        public BattleBuffAction(BattleBuffActionProfile profile, BattleActor actor)
        {
            _actor = actor;
            _profile = profile;
        }

        public virtual void OnBegin() { }
        public virtual void OnTick() { }
        public virtual void OnEnd() { }
        public virtual void Update() { }

        public static BattleBuffAction Create(BattleBuffActionProfile profile, BattleActor actor)
        {
            switch(profile.Type)
            {
                case BuffActionType.Interruption:
                    return new BattleBuffAction_Interruption(profile, actor);
                case BuffActionType.Airborn:
                    return new BattleBuffAction_Airborn(profile, actor);
            }

            return null;
        }
    }

    public enum BuffActionType
    {
        Invalid,
        Interruption,
        Airborn,
    }
}
