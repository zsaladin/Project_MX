using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class BattleActor : MonoBehaviour
    {
        private ActorRecord _record;
        private BattleActorTypeProfile _actorTypeProfile;
        private List<UIMonoBehaviour> _uiControllers = new List<UIMonoBehaviour>();

        public BattleActorProfile Profile { get; private set; }
        public BattleActorPropery Property { get; private set; }
        public BattleActorBaseAction BaseAction { get; private set; }

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

        public AnimationController AnimationController { get; private set; }

        public bool debug = false;

        public void Init(ActorRecord record, Ownership ownerShip, Vector3? position)
        {
            OwnerShip = ownerShip;

            _record = record;
            Profile = Manager.Data.ActorProfileSave.Get(record.ProfileID);
            _actorTypeProfile = Manager.Data.ActorTypeProfileSave.Get(Profile.ActorType);

            if (position != null)
                Position = position.Value;
            else
                Position = _record.Position;

            transform.position = Position;

            AnimationController = GetComponent<AnimationController>();
            AnimationController.Init();

            BaseAction = new BattleActorBaseAction(this);
            Property = new BattleActorPropery(this);

            StateMachine = new BattleStateMachine(this, _actorTypeProfile);
            SkillMachine = new BattleSkillMachine(this, Profile);
            BuffMachine = new BattleBuffMachine(this);
        }


        public void OnTick()
        {
            BuffMachine.OnTick();
            StateMachine.OnTick();
            SkillMachine.OnTick();
        }

        void Update()
        {
            BuffMachine.Update();
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