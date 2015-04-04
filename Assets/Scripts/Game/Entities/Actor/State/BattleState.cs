using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class BattleState : ITickable
    {
        public BattleStateProfile Profile { get; private set; }
        private BattleActor _actor;
        private BattleStateMachine _machine;
        private Dictionary<BattleStateCondition, BattleState> _conditions = new Dictionary<BattleStateCondition, BattleState>();
        private BattleAction_SearchForTarget _searchingTargetAction;

        private List<BattleAction> _actions;

        public BattleActor Target
        {
            get
            {
                if (_searchingTargetAction == null) return null;
                return _searchingTargetAction.Target;
            }
        }

        public Vector3 Destination
        {
            get
            {
                if (_searchingTargetAction == null) return _actor.Position;
                if (_searchingTargetAction.Target == null) return _actor.Position;
                return _searchingTargetAction.Target.Position;
            }
        }

        public void Init(BattleStateProfile profile, BattleActor actor, BattleStateMachine machine)
        {
            _actor = actor;
            Profile = profile;
            _machine = machine;

            _actions = new List<BattleAction>();

            for (int i = 0; i < profile.Actions.Count; ++i)
            {
                BattleAction action = BattleAction.Create(profile.Actions[i], _actor);
                _actions.Add(action);

                if (action is BattleAction_SearchForTarget) _searchingTargetAction = action as BattleAction_SearchForTarget;
            }
        }

        public void PostInit()
        {
            for (int i = 0; i < Profile.ConditionStateSet.Count; ++i)
            {
                ConditionStateSet set = Profile.ConditionStateSet[i];
                BattleStateConditionProfile conditionProfile = set.ConditionProfile;
                BattleStateCondition condition = BattleStateCondition.Create(conditionProfile, _actor, this);
                _conditions.Add(condition, _machine.States.FirstOrDefault(item => item.Profile.ID == set.StateProfileID));
            }
        }

        public void OnBegin()
        {
            for (int i = 0; i < _actions.Count; ++i)
            {
                _actions[i].OnBegin();
            }
        }

        public void OnTick()
        {
            for (int i = 0; i < _actions.Count; ++i)
            {
                _actions[i].OnTick();
            }
        }

        public void OnEnd()
        {
            for (int i = 0; i < _actions.Count; ++i)
            {
                _actions[i].OnEnd();
            }
        }

        public void Update()
        {
            for (int i = 0; i < _actions.Count; ++i)
            {
                _actions[i].Update();
            }
        }

        public BattleState OnConditionConfirm()
        {
            foreach (KeyValuePair<BattleStateCondition, BattleState> pair in _conditions)
            {
                BattleStateCondition condition = pair.Key;
                if (condition.IsInCondition())
                {
                    return pair.Value;
                }
            }
            return null;
        }

        public TAction FindAction<TAction>() where TAction : BattleAction
        {
            return _actions.Find(item => item is TAction) as TAction;
        }

        public BattleAction FindAction(ActionType type)
        {
            return _actions.Find(item => item.Type == type);
        }
    }
}