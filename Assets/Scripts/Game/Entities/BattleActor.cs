using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BattleActor : MonoBehaviour, ITickable
{
    private ActorRecord _record;
    private ActorProfile _profile;
    private ActorTypeProfile _actorTypeProfile;
    private BattleState _currentState;
    
    

    #region Battle Stat Property
    public float Size { get; protected set; }
    public float HitPointMax { get; protected set; }
    public float HitPoint { get; set; }

    public OffenseType OffenseType { get; protected set; }
    public float OffensePower { get; protected set; }
    public float OffenseTime { get; protected set; }
    public float OffenseDealTime { get; protected set; }
    public float OffenseRange { get; protected set; }

    public DefenseType DefenseType { get; protected set; }

    public float MovingSpeed { get; protected set; }
    #endregion

    public BattleState[] States { get; protected set; }

    public Ownership OwnerShip  { get; protected set; }
    public Ownership OpponentOwnerShip { get { return (Ownership)((int)OwnerShip ^ 0x1); } }

    public BattleActor Target { get { return _currentState.Target; } }

    public void Init(ActorRecord record, Ownership ownerShip, Vector3? position)
    {
        OwnerShip = ownerShip;

        _record = record;
        _profile = Manager.Data.ActorProfileSave.Get(record.ProfileID);
        _actorTypeProfile = Manager.Data.ActorTypeProfileSave.Get(_profile.ActorType);

        if (position != null)
            transform.position = position.Value;
        else
            transform.position = _record.Position;
        transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0));

        InitStats();
        InitStates();
    }

    void InitStats()
    {
        

        Size = _profile.Size;
        HitPointMax = HitPointMax = _profile.HitPointMax;
        OffenseType = _profile.OffenseType;
        OffensePower = _profile.OffensePower;
        OffenseTime = _profile.OffenseTime;
        OffenseDealTime = _profile.OffenseDealTime;
        OffenseRange = _profile.OffenseRange;

        DefenseType = _profile.DefenseType;

        MovingSpeed = _profile.MovingSpeed;
    }

    void InitStates()
    {
        States = new BattleState[_actorTypeProfile.States.Count];
        for(int i = 0; i < _actorTypeProfile.States.Count; ++i)
        {
            BattleStateProfile stateProfile = _actorTypeProfile.States[i];
            BattleState state = new BattleState();
            state.Init(stateProfile, this);
            States[i] = state;
        }

        for (int i = 0; i < States.Length; ++i)
        {
            States[i].PostInit();
        }
        _currentState = States[0];
    }

	public void OnTick()
    {
        _currentState.OnTick();
        BattleState nextState = _currentState.OnConditionConfirm();
        if (nextState != null)
        {
            _currentState.OnEnd();
            _currentState = nextState;
            _currentState.OnBegin();
        }
    }

    public void OnChangeState()
    {

    }
}
