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

        public BattleStateMachine StateMachine { get; private set; }
        public BattleSkillMachine SkillMachine { get; private set; }
        public BattleBuffMachine BuffMachine { get; private set; }

        public Ownership OwnerShip { get; private set; }
        public Ownership OpponentOwnerShip { get { return (Ownership)((int)OwnerShip ^ 0x1); } }

        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }

        public Vector3 LauncherPoint { get; set; }
        public Vector3 ProjectileHitPoint { get; set; }

        public BattleActor Target { get { return StateMachine.CurrentState.Target; } }
        public Vector3 Destination { get { return StateMachine.CurrentState.Destination; } }

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

            InitStats();

            AnimationController = GetComponent<AnimationController>();
            AnimationController.Init();

            BaseAction = new BattleActorBaseAction(this);

            StateMachine = new BattleStateMachine(this, _actorTypeProfile);
            SkillMachine = new BattleSkillMachine(this, _profile);
            BuffMachine = new BattleBuffMachine(this);

            
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

        public void OnTick()
        {
            BuffMachine.OnTick();
            StateMachine.OnTick();
            SkillMachine.OnTick();
        }

        void Update()
        {
            StateMachine.CurrentState.Update();
            BaseAction.Update();
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

#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            UnityEditor.Handles.Label(transform.position, StateMachine.CurrentState.Profile.Name);
            for(int i = 0; i < BuffMachine.Buffs.Count; ++i)
            {
                UnityEditor.Handles.Label(transform.position + Vector3.up * 1, BuffMachine.Buffs[i].Profile.Name);
            }
        }
#endif
    }
}