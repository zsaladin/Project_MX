using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class BattleBuff 
    {
        private BattleActor _actor;
        public BattleBuffProfile Profile { get; private set; }

        private List<BattleBuffAction> _buffActions = new List<BattleBuffAction>();
        private List<BattleBuffCondition> _buffConditions = new List<BattleBuffCondition>();

        public BattleBuff(BattleBuffProfile profile, BattleActor actor)
        {
            _actor = actor;
            Profile = profile;

            for(int i = 0; i < Profile.Actions.Count; ++i)
            {
                var buffAction = BattleBuffAction.Create(Profile.Actions[i], _actor);
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