using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BattleActor : MonoBehaviour, ITickable
{
    private ActorRecord _record;
    private ActorProfile _profile;
    private ActorTypeProfile _actorTypeProfile;

    private List<UIMonoBehaviour> _uiControllers = new List<UIMonoBehaviour>();
    
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
    public BattleState CurrentState { get; private set; }

    public Ownership OwnerShip  { get; protected set; }
    public Ownership OpponentOwnerShip { get { return (Ownership)((int)OwnerShip ^ 0x1); } }

    public BattleActor Target { get { return CurrentState.Target; } }
    public Vector3 Destination { get { return CurrentState.Destination; } }

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
        HitPoint = HitPointMax = _profile.HitPointMax;
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
        CurrentState = States[0];
        CurrentState.OnBegin();
    }

    public void AddUIController(UIMonoBehaviour ui)
    {
        _uiControllers.Add(ui);
    }

    public void RedrawUIs()
    {
        for(int i = 0; i < _uiControllers.Count; ++i)
        {
            _uiControllers[i].Redraw();
        }
    }

    public void EnableUIs(bool enable)
    {
        for (int i = 0; i < _uiControllers.Count; ++i)
        {
            _uiControllers[i].EnableUI(enable);
        }
    }

	public void OnTick()
    {
        CurrentState.OnTick();
        BattleState nextState = CurrentState.OnConditionConfirm();
        if (nextState != null)
        {
            CurrentState.OnEnd();
            CurrentState = nextState;
            CurrentState.OnBegin();
        }
    }

    void Update()
    {
        CurrentState.Update();
    }

    public void OnChangeState()
    {

    }
}
