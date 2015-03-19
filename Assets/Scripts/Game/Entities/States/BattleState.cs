using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BattleState : ITickable 
{
    private BattleStateProfile _profile;
    private BattleActor _actor;
    private List<BattleAction> _actions = new List<BattleAction>();
    private Dictionary<BattleCondition, BattleState> _conditions = new Dictionary<BattleCondition,BattleState>();

    private BattleAction_SearchForTarget _searchingTargetAction;

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
            if (_searchingTargetAction == null) return _actor.transform.position;
            if ( _searchingTargetAction.Target == null) return _actor.transform.position;
            return _searchingTargetAction.Target.transform.position;
        }
    }

    public void Init(BattleStateProfile profile, BattleActor actor)
    {
        _actor = actor;
        _profile = profile;

        for(int i = 0; i < profile.Actions.Count; ++i)
        {
            BattleAction action = BattleAction.Create(profile.Actions[i]);
            action.Actor = _actor;
            _actions.Add(action);

            if (action is BattleAction_SearchForTarget) _searchingTargetAction = action as BattleAction_SearchForTarget;
        }
    }

    public void PostInit()
    {
        for(int i = 0; i < _profile.ConditionStateSet.Count; ++i)
        {
            ConditionStateSet set = _profile.ConditionStateSet[i];
            BattleConditionProfile conditionProfile = Manager.Data.ConditionProfileSave.GetConditionProfile(set.ConditionType, set.ConditionProfileID);
            BattleCondition condition = BattleCondition.Create(set.ConditionType);
            condition.Init(conditionProfile, _actor, this);
            _conditions.Add(condition, _actor.States.FirstOrDefault(item => item._profile.ID == set.StateProfileID));
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
        foreach(KeyValuePair<BattleCondition, BattleState> pair in _conditions)
        {
            BattleCondition condition = pair.Key;
            if (condition.IsInCondition())
            {
                return pair.Value;   
            }
        }
        return null;
    }
}