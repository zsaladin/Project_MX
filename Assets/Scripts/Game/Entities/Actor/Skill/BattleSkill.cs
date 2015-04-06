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
        protected Vector3 _targetPosition;

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

            OnSkillEffects();
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

            OnHitEffects();
            OnSpotEffects();
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

        protected void OnSkillEffects()
        {
            for(int i = 0; i < Profile.OnSkillEffects.Count; ++i)
            {
                var effectProfile = Profile.OnSkillEffects[i];
                GameObject effectObject = GameObject.Instantiate<GameObject>(effectProfile.Prefab);
                effectObject.AddComponent<EffectController>().Init(effectProfile, Actor);
            }
        }

        protected void OnHitEffects()
        {
            for (int i = 0; i < Profile.OnHitEffects.Count; ++i)
            {
                var effectProfile = Profile.OnHitEffects[i];
                for (int j = 0; j < _targetActors.Count; ++j)
                {
                    GameObject effectObject = GameObject.Instantiate<GameObject>(effectProfile.Prefab);
                    if (_targetActors[j] != null)
                        effectObject.AddComponent<EffectController>().Init(effectProfile, _targetActors[j]);
                }
            }
        }

        protected void OnSpotEffects()
        {
            for (int i = 0; i < Profile.OnSpotEffects.Count; ++i)
            {
                var effectProfile = Profile.OnSpotEffects[i];
                GameObject effectObject = GameObject.Instantiate<GameObject>(effectProfile.Prefab);
                effectObject.AddComponent<EffectController>().Init(effectProfile, _targetPosition);
            }
        }


        public static BattleSkill Create(BattleSkillProfile profile, BattleActor actor)
        {
            switch (profile.Type)
            {
                case SkillType.Bomb:
                    return new BattleSkill_Bomb(profile, actor);
                case SkillType.Splash:
                    return new BattleSkill_Splash(profile, actor);
                case SkillType.BuffSelf:
                    return new BattleSkill_BuffSelf(profile, actor);
            }

            return null;
        }
    }


    public enum SkillType
    {
        Invalid,
        Bomb,
        Splash,
        BuffSelf,
    }
}