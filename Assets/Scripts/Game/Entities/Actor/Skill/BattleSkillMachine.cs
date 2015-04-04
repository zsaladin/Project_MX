using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class BattleSkillMachine : ITickable
    {
        private BattleActor _actor;
        private ActorProfile _profile;

        public BattleSkill[] Skills { get; private set; }
        public BattleSkill NextSkill { get; set; }

        public BattleSkillMachine(BattleActor actor, ActorProfile profile)
        {
            _actor = actor;
            _profile = profile;

            Skills = new BattleSkill[_profile.Skills.Count];
            for (int i = 0; i < _profile.Skills.Count; ++i)
            {
                BattleSkill skill = BattleSkill.Create(_profile.Skills[i], _actor);
                Skills[i] = skill;
            }
        }

        public void OnTick()
        {
            for (int i = 0; i < Skills.Length; ++i)
            {
                Skills[i].OnTick();
            }   
        }
    }
}
