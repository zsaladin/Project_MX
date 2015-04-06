using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class BattleBuffAction_MovementSpeed : BattleBuffAction
    {
        public override BuffActionType Type
        {
            get { return BuffActionType.MovementSpeed; }
        }

        public RatioValueType RatioValueType { get; private set; }
        public float Value { get; private set; }

        public BattleBuffAction_MovementSpeed(BattleBuffActionProfile profile, BattleActor actor, BattleActor caster)
            : base(profile, actor, caster)
        {
            RatioValueType = profile.Params.GetRatioValue("RatioValueType").Value;
            Value = profile.Params.GetFloat("Value").Value;
        }
    }
}