using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BattleState : ITickable 
{
    private BattleStateProfile _profile;
    private BattleActor _actor;
    private List<BattleAction> _actions = new List<BattleAction>();
    private Dictionary<BattleCondition, BattleState> _conditions = new Dictionary<BattleCondition,BattleState>();

    public BattleActor Target 
    {
        get 
        {
            BattleAction_SearchForTarget searchingAction = _actions.Find(item => item is BattleAction_SearchForTarget) as BattleAction_SearchForTarget;
            if (searchingAction == null) return null;
            return searchingAction.Target;
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
        int count = _actions.Count;
        for (int i = 0; i < count; ++i)
        {
            _actions[i].OnBegin();
        }
    }

    public void OnTick()
    {
        int count = _actions.Count;
        for(int i = 0; i < count; ++i)
        {
            _actions[i].OnTick();
        }
    }

    public void OnEnd()
    {
        int count = _actions.Count;
        for (int i = 0; i < count; ++i)
        {
            _actions[i].OnEnd();
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