using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class BattleBuff 
    {
        private BattleActor _actor;
        private BattleActor _attacker;
        public BattleBuffProfile Profile { get; private set; }

        private List<BattleBuffAction> _buffActions = new List<BattleBuffAction>();
        private List<BattleBuffCondition> _buffConditions = new List<BattleBuffCondition>();
        private List<EffectController> _effects = new List<EffectController>();

        public BattleBuff(BattleBuffProfile profile, BattleActor actor, BattleActor attacker)
        {
            _actor = actor;
            _attacker = attacker;
            Profile = profile;

            for(int i = 0; i < Profile.Actions.Count; ++i)
            {
                if (_actor.BuffMachine.ContainsBuff(Profile.Actions[i].Type))
                    continue;
                var buffAction = BattleBuffAction.Create(Profile.Actions[i], _actor, attacker);
                _buffActions.Add(buffAction);
            }

            for(int i = 0; i < Profile.Conditions.Count; ++i)
            {
                var buffCondition = BattleBuffCondition.Create(Profile.Conditions[i], _actor);
                _buffConditions.Add(buffCondition);
            }
        }

        public void OnBegin()
        {
            for(int i = 0; i < _buffActions.Count; ++i)
            {
                _buffActions[i].OnBegin();
            }

            for(int i = 0; i < _buffConditions.Count; ++i)
            {
                _buffConditions[i].OnBegin();
            }

            for(int i = 0; i < Profile.Effects.Count; ++i)
            {
                var effectProfile = Profile.Effects[i];
                GameObject effectObject = GameObject.Instantiate<GameObject>(effectProfile.Prefab);
                EffectController effect = effectObject.AddComponent<EffectController>();
                effect.Init(effectProfile, _actor);
                _effects.Add(effect);
            }
        }

        public void OnTick()
        {
            for(int i = 0; i < _buffActions.Count; ++i)
            {
                _buffActions[i].OnTick();
            }

            for (int i = 0; i < _buffConditions.Count; ++i)
            {
                _buffConditions[i].OnTick();
            }
        }

        public void OnEnd()
        {
            for(int i = 0; i < _buffActions.Count; ++i)
            {
                _buffActions[i].OnEnd();
            }

            for (int i = 0; i < _buffConditions.Count; ++i)
            {
                _buffConditions[i].OnEnd();
            }

            for(int i = 0; i < _effects.Count; ++i)
            {
                _effects[i].Destroy();
            }
        }

        public void Update()
        {
            for(int i = 0; i <_buffActions.Count; ++i)
            {
                _buffActions[i].Update();
            }
        }

        public bool IsEnded
        {
            get
            {
                for (int i = 0; i < _buffConditions.Count; ++i)
                {
                    if (_buffConditions[i].IsInCondition() == false)
                        return false;
                }
                return true;
            }
        }

        public bool ContainsAction(BuffActionType type)
        {
            return _buffActions.Find(item => item.Type == type) != null;
        }
    }
}