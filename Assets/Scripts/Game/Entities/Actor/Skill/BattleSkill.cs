using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public abstract class BattleSkill : ITickable
    {
        protected BattleSkillProfile Profile { get; private set; }
        protected BattleActor Actor { get; private set; }

        protected List<BattleSkillCondition> _conditions = new List<BattleSkillCondition>();
        protected List<BattleActor> _targetActors = new List<BattleActor>();

        protected float _damage;

        public BattleSkill(BattleSkillProfile profile, BattleActor actor)
        {
            Profile = profile;
            Actor = actor;

            for (int i = 0; i < profile.Conditions.Count; ++i)
            {
                _conditions.Add(BattleSkillCondition.Create(profile.Conditions[i], actor, this));
            }
        }

        public virtual void OnSkill()
        {
            for (int i = 0; i < _conditions.Count; ++i)
                _conditions[i].OnSkill();
        }

        public virtual void OnTick()
        {
            for (int i = 0; i < _conditions.Count; ++i)
            {
                _conditions[i].OnTick();
            }
        }

        public virtual void Deal()
        {
            for(int i = 0; i < _targetActors.Count; ++i)
            {
                var defenseAction = _targetActors[i].StateMachine.CurrentState.FindAction<BattleAction_Defense>();
                if (defenseAction != null)
                    defenseAction.Defense(_damage);

                for (int j = 0; j < Profile.Buffs.Count; ++j)
                    _targetActors[i].BuffMachine.AddBuff(Profile.Buffs[j], Actor);
            }
            _targetActors.Clear();
        }

        public bool IsInCondition()
        {
            for (int i = 0; i < _conditions.Count; ++i)
            {
                if (_conditions[i].IsInCondition() == false)
                    return false;
            }

            return true;
        }

        public static BattleSkill Create(BattleSkillProfile profile, BattleActor actor)
        {
            switch (profile.Type)
            {
                case SkillType.Bomb:
                    return new BattleSkill_Bomb(profile, actor);
                case SkillType.Splash:
                    return new BattleSkill_Splash(profile, actor);
            }

            return null;
        }
    }


    public enum SkillType
    {
        Invalid,
        Bomb,
        Splash,
    }
}