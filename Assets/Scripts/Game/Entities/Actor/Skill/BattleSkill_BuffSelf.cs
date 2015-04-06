using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class BattleSkill_BuffSelf : BattleSkill
    {
        public BattleSkill_BuffSelf(BattleSkillProfile profile, BattleActor actor)
            : base(profile, actor)
        {
            
        }

        public override void OnSkill()
        {
            base.OnSkill();

            _targetActors.Add(Actor);
            Deal();
        }
    }
}