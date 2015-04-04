using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class BattleActor : MonoBehaviour, ITickable
    {
        private ActorRecord _record;
        private ActorProfile _profile;
        private ActorTypeProfile _actorTypeProfile;

        private List<UIMonoBehaviour> _uiControllers = new List<UIMonoBehaviour>();

        #region Battle Stat Property
        public float Size { get; private set; }
        public float HitPointMax { get; private set; }
        public float HitPoint { get; set; }

        public OffenseType OffenseType { get; private set; }
        public int OffenseProjectileType { get; private set; }
        public float OffensePower { get; private set; }
        public float OffenseTime { get; private set; }
        public float OffenseDealTime { get; private set; }
        public float OffenseRange { get; private set; }

        public DefenseType DefenseType { get; private set; }

        public float MovingSpeed { get; private set; }
        #endregion

        public BattleState[] States { get; private set; }
        public BattleState CurrentState { get; private set; }
        public BattleState DefaultState { get; private set; }

        public BattleSkill[] Skills { get; private set; }

        public Ownership OwnerShip { get; private set; }
        public Ownership OpponentOwnerShip { get { return (Ownership)((int)OwnerShip ^ 0x1); } }

        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }

        public Vector3 LauncherPoint { get; set; }
        public Vector3 ProjectileHitPoint { get; set; }

        public BattleActor Target { get { return CurrentState.Target; } }
        public Vector3 Destination { get { return CurrentState.Destination; } }

        public BattleActorBaseAction BaseAction { get; private set; }
        public AnimationController AnimationController { get; private set; }

        public void Init(ActorRecord record, Ownership ownerShip, Vector3? position)
        {
            OwnerShip = ownerShip;

            _record = record;
            _profile = Manager.Data.ActorProfileSave.Get(record.ProfileID);
            _actorTypeProfile = Manager.Data.ActorTypeProfileSave.Get(_profile.ActorType);

            if (position != null)
                Position = position.Value;
            else
                Position = _record.Position;

            transform.position = Position;
            //transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0));

            AnimationController = GetComponent<AnimationController>();
            AnimationController.Init();

            BaseAction = new BattleActorBaseAction(this);

            InitStats();
            InitStates();
            InitSkills();
        }

        void InitStats()
        {
            Size = _profile.Size;
            HitPoint = HitPointMax = _profile.HitPointMax;
            OffenseType = _profile.OffenseType;
            OffenseProjectileType = _profile.OffenseProjectileType;
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
            for (int i = 0; i < _actorTypeProfile.States.Count; ++i)
            {
                BattleStateProfile stateProfile = _actorTypeProfile.States[i];
                BattleState state = new BattleState();
                state.Init(stateProfile, this);
                States[i] = state;

                if (stateProfile.IsDefault) 
                    DefaultState = state;
            }

            for (int i = 0; i < States.Length; ++i)
            {
                States[i].PostInit();
            }
            CurrentState = DefaultState;
            CurrentState.OnBegin();
        }

        void InitSkills()
        {
            Skills = new BattleSkill[_profile.Skills.Count];
            for (int i = 0; i < _profile.Skills.Count; ++i)
            {
                BattleSkill skill = BattleSkill.Create(_profile.Skills[i], this);
                Skills[i] = skill;
            }
        }

        public void AddUIController(UIMonoBehaviour ui)
        {
            _uiControllers.Add(ui);
        }

        public void RedrawUIs()
        {
            for (int i = 0; i < _uiControllers.Count; ++i)
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

            for (int i = 0; i < Skills.Length; ++i)
            {
                Skills[i].OnTick();
            }
        }

        void Update()
        {
            CurrentState.Update();
            BaseAction.Update();
        }

        public void OnChangeState()
        {

        }
    }
}